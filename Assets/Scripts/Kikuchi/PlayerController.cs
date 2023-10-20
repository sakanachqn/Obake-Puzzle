using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using DG.Tweening;

/// <summary>
/// Playerの入力及び移動周り処理用クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    //デッドゾーン設定用変数
    [SerializeField]
    private float deadZone = 0.5f;
    ControllerInput inp;
    //移動処理判定用フラグ
    private bool isMoveNow = false;
    //回転処理判定用フラグ
    private bool isRotateNow = false;
    private bool isCamNow = false;
    //スティックの傾き保存用変数
    private Vector2 stickInclination = Vector2.zero;
    //正面方向取得用クラス格納用変数
    [SerializeField]
    private ObjectRotation objectRotation;
    //回転させる親オブジェクト
    [SerializeField]
    private GameObject camParent;
    //回転にかかる時間
    [SerializeField]
    private float rotateTime = 2f;
    [SerializeField]
    private float moveTime = 2f;

    //移動先情報確認用クラス
    private ObjectCheck objectCheck;

    /// <summary>
    /// スティックの倒れた方向
    /// </summary>
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Null
    };

    /// <summary>
    /// enumに対応したvec3を保存する変数
    /// </summary>
    private Dictionary<Direction, Vector3> directions = new Dictionary<Direction, Vector3>();

    //スティックの倒れた方向保存用変数
    private Direction stickDirection = Direction.Null;

    /// <summary>
    /// enumに対応したVec3を保存する関数
    /// </summary>
    /// <param name="foward">Vec3</param>
    /// <param name="back">Vec3</param>
    /// <param name="left">Vec3</param>
    /// <param name="right">Vec3</param>
    private void SetDirectionDictionary(Vector3 foward, Vector3 back, Vector3 left, Vector3 right)
    {
        directions[Direction.Left] = left;
        directions[Direction.Right] = right;
        directions[Direction.Up] = foward;
        directions[Direction.Down] = back;
    }

    /// <summary>
    /// ボタンの押された方
    /// </summary>
    private enum LeftRight
    {
        Left,
        Right,
        Null
    };
    private LeftRight camLR = LeftRight.Null;



    // Start is called before the first frame update
    async void Start()
    {
        //FPSの固定化(60);
        Application.targetFrameRate = 60;
        // inpSysの定義読み込み
        inp = ControllerManager.instance.CtrlInput;
        //オブジェクトの移動先確認クラス読み込み
        objectCheck = this.GetComponent<ObjectCheck>();
        //キャンセルトークンの取得
        var token = this.GetCancellationTokenOnDestroy();

        //Dictonary Init
        var vec3s = objectRotation.SetFoward();
        SetDirectionDictionary(vec3s[0], vec3s[1], vec3s[2], vec3s[3]);

        // inpSysの有効化
        inp.Enable();

　       await UniTask.WhenAll(MoveTask(token), CamRotateTask(token));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoveNow && !isRotateNow && !isCamNow)CheckStickDirection();
        if(!isMoveNow && !isRotateNow && !isCamNow)CheckPressCamRotateButton();
    }

    #region 移動

    /// <summary>
    /// 移動コルーチン
    /// </summary>
    private async UniTask MoveTask(CancellationToken token)
    { 
        while (true)
        {
            if (isMoveNow || isRotateNow || isCamNow) return; //移動中or回転中は早期リターン
            await UniTask.WaitUntil(() => stickDirection != Direction.Null, cancellationToken: token);// stickが倒されるのを待つ
            isMoveNow = true; //移動中フラグ起動
            var targetPos = this.transform.position + directions[stickDirection]; //目標地点を設定
            objectCheck.CheckObject(targetPos);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            while (isMoveNow)
            {
                Move(targetPos); //移動
                if (this.transform.position == targetPos) isMoveNow = false; //移動しきったらフラグoff
                await UniTask.DelayFrame(1, cancellationToken: token);　//1f待つ
            }
        }

    }



    /// <summary>
    /// 引数で渡された座標まで移動
    /// </summary>
    /// <param name="targetPos">目標地点 Vector3</param>
    private void Move(Vector3 targetPos)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime / moveTime);
    }


    /// <summary>
    /// 移動方向取得関数
    /// </summary>
    private void CheckStickDirection()
    {
        //スティックの入力取得
        stickInclination = inp.Player.Move.ReadValue<Vector2>();
        //正規化
        stickInclination = DeadZone(stickInclination);
        // 取得したスティックの方向に対応したenumに変換
        if (stickInclination == Vector2.zero) stickDirection = Direction.Null;
        else if (stickInclination.x == -1) stickDirection = Direction.Left;
        else if (stickInclination.x == 1) stickDirection = Direction.Right;
        else if (stickInclination.y == 1) stickDirection = Direction.Up;
        else if (stickInclination.y == -1) stickDirection = Direction.Down;

    }

    private Vector2 DeadZone(Vector2 vec2)
    {
        vec2.x = DeadZoneCheck(vec2.x);
        vec2.y = DeadZoneCheck(vec2.y);
        return vec2;
    }

    /// <summary>
    /// 引数が絶対値0.5以下かどうか
    /// </summary>
    /// <param name="value"></param>
    /// <returns>0.5未満なら0 / 0.5以上なら1 or -1</returns>
    private int DeadZoneCheck(float value)
    {
        bool minus = false;
        if(value < 0) minus = true;
        float abs = Mathf.Abs(value);
        if (abs <= deadZone) return 0;
        else if(minus) return -1;
        else return 1;
    }
    #endregion

    #region 回転

    /// <summary>
    /// 回転コルーチン
    /// </summary>
    /// <param name="token"></param>
    private async UniTask CamRotateTask(CancellationToken token)
    {
        while (true)
        {
            if (isMoveNow || isRotateNow || isCamNow) return; //移動中or回転中は早期リターン
            await UniTask.WaitUntil(() => camLR != LeftRight.Null, cancellationToken: token);　//enumがnull以外になったら
            isRotateNow = true;//フラグ起動
            var targetRotate = SetCamTargetRotete();//回転量設定
            await camParent.transform.DORotate(targetRotate, rotateTime, RotateMode.LocalAxisAdd);//回転処理&回転終わるまで待機
            camLR = LeftRight.Null;//enum初期化
            //Dictionaryの各方向のVec3を再設定
            var vec3s = objectRotation.SetFoward();
            SetDirectionDictionary(vec3s[0], vec3s[1], vec3s[2], vec3s[3]);
            //
            isRotateNow = false;//フラグoff
        }
    }

    /// <summary>
    /// L1 or R1が押されたかどうかの判定用関数
    /// </summary>
    private void CheckPressCamRotateButton()
    {
        if (inp.Rotate.CamL.WasPressedThisFrame()) camLR = LeftRight.Left;
        else if (inp.Rotate.CamR.WasPressedThisFrame()) camLR = LeftRight.Right;
        else camLR = LeftRight.Null;
    }

    /// <summary>
    /// 回転方向設定用関数
    /// </summary>
    /// <returns>右左どちらか90°</returns>
    private Vector3 SetCamTargetRotete()
    {
        Vector3 nowRotate = Vector3.zero;
        if (camLR == LeftRight.Right) nowRotate.y = -90f;
        else if (camLR == LeftRight.Left) nowRotate.y = 90f;
        return nowRotate;
    }

    #endregion
}
