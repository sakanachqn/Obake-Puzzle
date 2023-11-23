using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{

    private ControllerManager ctrlManager;
    [SerializeField]
    private Material material;
    private Color testRed = new Color(1f, 0, 0, 0.5f);
    private Color testColor;

    private bool nowMove = false;
    
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

    private void Start()
    {
        ctrlManager = ControllerManager.instance;
        material = GetComponent<MeshRenderer>().material;
        testColor = material.color;
    }
    /// <summary>
    /// 移動コルーチン
    /// </summary>
    public async UniTask MoveTask(CancellationToken token, float speed)
    {
        //移動中or回転中は早期リターン
        if (PlayerController.IsNowAction || SkillManager.IsNowSkill) return;

        if (ctrlManager.stickPlayerDirection != ControllerManager.Direction.Null && !PlayerController.IsNowAction)
        {
            await MoveCoroutine(token, speed);
        }


    }

    public async void PLMoveUpdate()
    {
        await MoveTask(this.GetCancellationTokenOnDestroy(), 2f);
    }

    private async UniTask MoveCoroutine(CancellationToken token, float speed)
    {
        PlayerController.IsNowAction = true; //移動中フラグ起動
        var targetPos = this.transform.position + directions[ctrlManager.stickPlayerDirection]; //目標地点を設定
        if (CheckObject(this.transform.position, directions[ctrlManager.stickPlayerDirection]) || CheckOffMap(directions[ctrlManager.stickPlayerDirection]))
        {
            DevLog.Log("進めないよ～");
            material.color = testRed;
            await UniTask.Delay(500);
            material.color = testColor;
            PlayerController.IsNowAction = false;
            return;
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        await this.transform.DOMove(targetPos, speed).SetEase(Ease.Linear);
        PlayerController.IsNowAction = false; //移動しきったらフラグoff
        nowMove = false;
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
}
