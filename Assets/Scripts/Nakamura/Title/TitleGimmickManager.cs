using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleGimmickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obake;

    [SerializeField]
    private Vector3 obakePosition;

    [SerializeField]
    private int maxObakeCount = 30;

    private int obakeCount = 0;

    [SerializeField]
    private string GotoMapSelect;

    /// <summary>
    /// ボタンが押されたら画面遷移する
    /// 最大数までボタンが押されたらオバケが生成される
    /// </summary>
    private void Update()
    {
        // ボタンが押されたら画面遷移する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.SelectMap.WasPressedThisFrame())
        {
            SceneManager.LoadScene(GotoMapSelect);
        }

        //オバケが最大数超えたら
        if (obakeCount > maxObakeCount) return;

        //ボタン押されたらオバケを生成する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.Obake.WasPressedThisFrame())
        {
            Instantiate(obake, obakePosition, Quaternion.identity);
            obakeCount++;
        }
    }
}
