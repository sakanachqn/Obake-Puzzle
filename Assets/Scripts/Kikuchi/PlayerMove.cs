using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private ControllerManager ctrlManager;

    /// <summary>
    /// enumに対応したvec3を保存する変数
    /// </summary>
    private Dictionary<ControllerManager.Direction, Vector3> directions = new Dictionary<ControllerManager.Direction, Vector3>();

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
    }
    /// <summary>
    /// 移動コルーチン
    /// </summary>
    public async UniTask MoveTask(CancellationToken token, float speed)
    {
        while (true)
        {
            if (PlayerController.isNowAction) return;
            //移動中or回転中は早期リターン
            await UniTask.WaitUntil(() => ctrlManager.stickDirection != ControllerManager.Direction.Null, cancellationToken: token);// stickが倒されるのを待つ
            PlayerController.isNowAction = true; //移動中フラグ起動
            var targetPos = this.transform.position + directions[ctrlManager.stickDirection]; //目標地点を設定
            if (CheckObject(directions[ctrlManager.stickDirection]))
            {
                DevLog.Log("進めないよ～");
                PlayerController.isNowAction = false;
                continue;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            while (PlayerController.isNowAction)
            {
                Move(targetPos, speed); //移動
                if (this.transform.position == targetPos) PlayerController.isNowAction = false; //移動しきったらフラグoff
                await UniTask.DelayFrame(1, cancellationToken: token);　//1f待つ
            }
        }
    }
    /// <summary>
    /// 引数で渡された座標まで移動
    /// </summary>
    /// <param name="targetPos">目標地点 Vector3</param>
    private void Move(Vector3 targetPos, float speed)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime / speed);
    }

    /// <summary>
    /// 移動先にobjectがあるかのチェック
    /// </summary>
    private bool CheckObject(Vector3 targetDirec)
    {
        Debug.DrawRay(this.transform.position, targetDirec, Color.cyan, 3);
        if (Physics.Raycast(this.transform.position, targetDirec, out var hitObj, 1))
        {
            Debug.Log(hitObj.collider.name);
            return true;
        }
        else return false;
    }
}
