using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    private PlayerController plCon;

    private ControllerManager ctrlManager;
    [SerializeField]
    private Material material;
    private Color testRed = new Color(1f, 0, 0, 0.5f);
    private Color testColor;

    private bool isGoal = false;
    [SerializeField]
    private int fadeInTime = 3;

    private GameObject parentCanv;
    private GameObject goalImg;
    private GameObject goalBG;

    public bool isWalkCount = false;
    private int walkCount = 0;
    public int WalkCount
    {
        get { return walkCount; }
        set { walkCount = value; }
    }
    
    /// <summary>
    /// enumに対応したvec3を保存する変数
    /// </summary>
    private Dictionary<ControllerManager.Direction, Vector3> directions = new Dictionary<ControllerManager.Direction, Vector3>();
    public Dictionary<ControllerManager.Direction, Vector3> Directions => directions;

    /// <summary>
    /// enumに対応したVec3を保存する関数
    /// </summary>
    public void SetDirectionDictionary(Vector3 foward, Vector3 back, Vector3 left, Vector3 right)
    {
        directions[ControllerManager.Direction.Left] = left;
        directions[ControllerManager.Direction.Right] = right;
        directions[ControllerManager.Direction.Up] = foward;
        directions[ControllerManager.Direction.Down] = back;
    }

    public void PlayerMoveStart()
    {
        ctrlManager = ControllerManager.instance;
        material = GetComponent<SkinnedMeshRenderer>().material;
        testColor = material.color;
        plCon = GetComponent<PlayerController>();
        parentCanv = GameObject.Find("UICanvas");
        goalImg = parentCanv.transform.Find("res").gameObject;
        goalImg.SetActive(false);
        goalBG = parentCanv.transform.Find("bg").gameObject;
        goalBG.SetActive(false);
    }


    public async void PLMoveUpdate(float speed)
    {
        await MoveTask(this.GetCancellationTokenOnDestroy(), speed);
    }

    public async void PLRotUpdate(float speed)
    {
        await RotTask(this.GetCancellationTokenOnDestroy(), speed);
    }

    /// <summary>
    /// 移動コルーチン
    /// </summary>
    public async UniTask MoveTask(CancellationToken token, float speed)
    {
        //移動中or回転中は早期リターン
        if (PlayerController.IsNowAction || SkillManager.IsNowSkill || SkillManager.isNowSuction) return;

        if (ctrlManager.stickPlayerDirection != ControllerManager.Direction.Null && !PlayerController.IsNowAction)
        {
            await MoveMethod(token, speed);
        }
    }

    private async UniTask RotTask(CancellationToken token, float speed)
    {
        if (PlayerController.IsNowAction || SkillManager.IsNowEffect) return;
        

        if (ctrlManager.dPadDirection != ControllerManager.Direction.Null && !PlayerController.IsNowAction)
        {
            await RotateDirection(directions[ctrlManager.dPadDirection], speed);
            
            if (SkillManager.isNowSuction)
            {
                plCon.SkillManager.CurrentSkillC.ReverseObject();
                isWalkCount = false;
            }
        }
    }

    private async UniTask MoveMethod(CancellationToken token, float speed)
    {
        PlayerController.IsNowAction = true; //移動中フラグ起動
        var targetPos = this.transform.position + directions[ctrlManager.stickPlayerDirection]; //目標地点を設定
        var targetDirection = directions[ctrlManager.stickPlayerDirection];
        if (CheckObject(this.transform.position, targetDirection) || CheckOffMap(targetDirection))
        {
            //進行不可のとき
            material.color = testRed;
            await UniTask.Delay(500);
            material.color = testColor;
            PlayerController.IsNowAction = false;
            return;
        }

        ObakeAnimation.Inctance.WalkAnimation(true);
        await RotateDirection(directions[ctrlManager.stickPlayerDirection], speed);
        await this.transform.root.transform.DOMove(targetPos, speed);
        ObakeAnimation.Inctance.WalkAnimation(false);
        if (isWalkCount)
        {
            walkCount++;
            Debug.Log(walkCount);
        }
        if(walkCount == 2)
        {
            Debug.Log("in");
            plCon.SkillManager.CurrentSkillC.ReverseObject();
            isWalkCount = false;
        }
        PlayerController.IsNowAction = false; //移動しきったらフラグoff
        if (isGoal)
        {
            await GoalCoroutine(this.GetCancellationTokenOnDestroy());
        }
    }


    /// <summary>
    /// 移動先にobjectがあるかのチェック
    /// </summary>
    /// <param name="targetDirec">スティックの方角</param>
    /// <returns>移動先にobjがあったらtrue</returns>
    private bool CheckObject(Vector3 startPos , Vector3 targetDirec)
    {
        //移動したい方向にrayを飛ばしてオブジェクトがあるか確認
        if (Physics.Raycast(startPos, targetDirec, out var hitObj, 1))
        {
            if (hitObj.collider.tag == "Pitfall") return false;
            if (hitObj.collider.tag == "Goal")
            {
                Debug.Log("hit");
                isGoal = true;
                return false;
            }
            return true;
        }
        else return false;
    }

    /// <summary>
    /// マップ外かどうかのチェック
    /// </summary>
    /// <param name="targetDirec">スティックの方角</param>
    /// <returns>オブジェクトがなかったらtrue</returns>
    private bool CheckOffMap(Vector3 targetDirec)
    {
        Vector3 startPos = this.transform.position + targetDirec;
        //移動先から真下にray飛ばして地面があるか確認
        if (CheckObject(startPos ,Vector3.down))
        {
            return false;
        }
        else return true;
    }

    /// <summary>
    /// 指定した方向に向けて回転する
    /// </summary>
    private async UniTask RotateDirection(Vector3 direction, float speed)
    {
        var targetRotation = Quaternion.LookRotation(direction);
        await this.transform.root.transform.DORotateQuaternion(targetRotation, speed);
    }


    private async UniTask GoalCoroutine(CancellationToken ct)
    {
        SoundManager.Instance.Play("ResSound");
        var bg = goalBG.GetComponent<Image>();
        goalBG.SetActive(true);
        await bg.DOFade(0.8f, 1);
        goalImg.SetActive(true);
        var img = goalImg.GetComponent<RectTransform>();
        await img.DOAnchorPosY(-20, fadeInTime);
        await UniTask.Delay(1000);
        goalImg.transform.Find("time").gameObject.SetActive(true);
        await UniTask.Delay(1000);
        goalImg.transform.Find("walk").gameObject.SetActive(true);
        await UniTask.Delay(1500);
        goalImg.transform.Find("rank").gameObject.SetActive(true);
        await UniTask.Delay(2000);
        await SceneFade.instance.SceneChange("ResScene");

    }

}
