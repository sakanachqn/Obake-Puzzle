using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class TitleGimmickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obake;

    [SerializeField]
    private Vector3 obakePosition;

    [SerializeField]
    private int maxObakeCount = 30;

    //moc版用修正/菊池
    public static int obakeCount = 0;

    [SerializeField]
    private string GotoMapSelect;


    private void Start()
    {
    }

    /// <summary>
    /// ボタンが押されたら画面遷移する
    /// 最大数までボタンが押されたらオバケが生成される
    /// </summary>
    private async void Update()
    {
        // ボタンが押されたら画面遷移する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.SelectMap.WasPressedThisFrame())
        {
            obakeCount = 0;
            await SceneFade.instance.SceneChange(GotoMapSelect);
        }

        //オバケが最大数超えたら
        if (obakeCount > maxObakeCount) return;

        //ボタン押されたらオバケを生成する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.Obake.WasPressedThisFrame())
        {
            Instantiate(obake, obakePosition, Quaternion.Euler(0, 180, 0));
            obakeCount++;
        }
    }
}
