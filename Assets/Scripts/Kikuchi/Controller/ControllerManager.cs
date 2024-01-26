using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コントローラー入力を管理するクラス
/// </summary>
public class ControllerManager : MonoBehaviour
{
    private static ControllerManager _instance;
    public static ControllerManager instance
    {
        get {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<ControllerManager>();

                if (_instance == null)
                {
                    var obj = new GameObject("ControllerManager");
                    _instance = obj.AddComponent<ControllerManager>();
                }
            }
            return _instance; 
        }
    }

    //スティックの傾き保存用変数
    private Vector2 stickInclination = Vector2.zero;

    //デッドゾーン設定用変数
    [SerializeField]
    private float deadZone = 0.5f;

    public ControllerInput CtrlInput;

    /// <summary>
    /// スティックの倒れた方向
    /// </summary>
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Null
    };

    //スティックの倒れた方向保存用変数
    public Direction stickPlayerDirection = Direction.Null;

    /// <summary>
    /// L1 or R1 ボタンの押された方
    /// </summary>
    public enum LeftRight
    {
        Left,
        Right,
        Null
    };
    public LeftRight camLR = LeftRight.Null;


    private void Awake()
    {
        if (instance == null)
        {
            _instance = this;
        }
        else Destroy(this.gameObject);
        if(CtrlInput == null)
        {
            CtrlInput = new ControllerInput();
            CtrlInput.Enable();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ControllerUpdate()
    {
        if(!PlayerController.IsNowAction)CheckPlayerStickDirection();
        if(!PlayerController.IsNowAction)CheckPressCamRotateButton();
    }

    #region stick
    /// <summary>
    /// スティックの倒れた方向取得関数
    /// </summary>
    private void CheckPlayerStickDirection()
    {
        //スティックの入力取得
        stickInclination = CtrlInput.Player.Move.ReadValue<Vector2>();
        //正規化
        stickInclination = DeadZone(stickInclination);
        // 取得したスティックの方向に対応したenumに変換
        if (stickInclination == Vector2.zero) stickPlayerDirection = Direction.Null;
        else if (stickInclination.x == -1) stickPlayerDirection = Direction.Left;
        else if (stickInclination.x == 1) stickPlayerDirection = Direction.Right;
        else if (stickInclination.y == 1) stickPlayerDirection = Direction.Up;
        else if (stickInclination.y == -1) stickPlayerDirection = Direction.Down;
    }

    //デッドゾーン設定用関数
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
        if (value < 0) minus = true;
        float abs = Mathf.Abs(value);
        if (abs <= deadZone) return 0;
        else if (minus) return -1;
        else return 1;
    }
    #endregion

    #region L1R1
    /// <summary>
    /// L1 or R1が押されたかどうかの判定用関数
    /// </summary>
    private void CheckPressCamRotateButton()
    {
        if (CtrlInput.Rotate.CamL.WasPressedThisFrame()) camLR = LeftRight.Left;
        else if (CtrlInput.Rotate.CamR.WasPressedThisFrame()) camLR = LeftRight.Right;
        else camLR = LeftRight.Null;
    }
    #endregion
}
