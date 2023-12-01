using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class TitleGimmickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obake;

    [SerializeField]
    private GameObject StartBtn;

    [SerializeField]
    private int maxObakeCount = 30;

    private int obakeCount = 0;

    [SerializeField]
    private string GotoMapSelect;

    private SceneFade sceneFade;

    private Vector3 obakePosition;

    private void Start()
    {
        sceneFade = new SceneFade();

        obakePosition = new Vector3(Random.Range(0f, 8.0f)
                                , Random.Range(2.62f, 6.52f)
                                , Random.Range(-0.25f, -2.0f));
    }

    /// <summary>
    /// ボタンが押されたら画面遷移する
    /// 最大数までボタンが押されたらオバケが生成される
    /// </summary>
    private async UniTask Update()
    {
        // ボタンが押されたら画面遷移する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.SelectMap.WasPressedThisFrame())
        {
            await StartBtn.transform.DOScale(new Vector3(StartBtn.gameObject.transform.position.x -2.0f
                                                        , StartBtn.gameObject.transform.position.y - 2.0f
                                                        , StartBtn.gameObject.transform.position.z)
                                                        , 1f);

            await sceneFade.SceneChange("GotoMapSelect");
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
