using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static ControllerManager;

public class PlayerRotate : MonoBehaviour
{

    private ControllerManager ctrlManager;

    [SerializeField]
    private GameObject rotateParent;

    private PlayerController playerController;

    public void PlayerRotateStart(float rotateTime)
    {
        ctrlManager = ControllerManager.instance;
        playerController = GetComponent<PlayerController>();
        if (rotateParent == null) rotateParent = Camera.main.transform.parent.gameObject;
        CamRotateTask(this.GetCancellationTokenOnDestroy(), rotateTime).Forget();
    }

    /// <summary>
    /// 回転コルーチン
    /// </summary>
    /// <param name="token"></param>
    public async UniTask CamRotateTask(CancellationToken token, float rotateTime)
    {
        while (true)
        {
            if (PlayerController.IsNowAction) return; //移動中or回転中は早期リターン
            await UniTask.WaitUntil(() => ctrlManager.camLR != LeftRight.Null, cancellationToken: token);　//enumがnull以外になったら
            PlayerController.IsNowAction = true;//フラグ起動
            var targetRotate = SetCamTargetRotete();//回転量設定
            await rotateParent.transform.DORotate(targetRotate, rotateTime, RotateMode.LocalAxisAdd);//回転処理&回転終わるまで待機
            ctrlManager.camLR = LeftRight.Null;//enum初期化
            //Dictionaryの各方向のVec3を再設定
            var vec3s = playerController.objRotate.SetFoward();
            playerController.plMove.SetDirectionDictionary(vec3s[0], vec3s[1], vec3s[2], vec3s[3]);
            //
            PlayerController.IsNowAction = false;//フラグoff
        }
    }

    /// <summary>
    /// 回転方向設定用関数
    /// </summary>
    /// <returns>右左どちらか90°</returns>
    private Vector3 SetCamTargetRotete()
    {
        Vector3 nowRotate = Vector3.zero;
        if (ctrlManager.camLR == LeftRight.Right) nowRotate.y = -90f;
        else if (ctrlManager.camLR == LeftRight.Left) nowRotate.y = 90f;
        return nowRotate;
    }
}
