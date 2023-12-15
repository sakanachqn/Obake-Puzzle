using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

public class TitleGimmickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obake;

    [SerializeField]
    private Image startBtn;

    [SerializeField]
    private int maxObakeCount = 30;

    //moc版用修正/菊池
    public static int obakeCount = 0;

    [SerializeField]
    private string GotoMapSelect;

    private Vector3 obakePosition;

    [SerializeField]
    private float width = 10.0f;

    private Vector3 startBtnPos;

    [SerializeField]
    private GameObject bowl;

    [SerializeField]
    private List<Material> bowlMaterials = new List<Material>();

    private void Start()
    {
        obakePosition = new Vector3(Random.Range(0f, 8.0f)
                                , Random.Range(2.62f, 6.52f)
                                , Random.Range(-0.25f, -2.0f));

        startBtnPos = startBtn.transform.position;

        //受け皿のマテリアルをランダムに変更
        int index = Random.Range(0,bowlMaterials.Count);
        bowl.GetComponent<MeshRenderer>().material = bowlMaterials[index];
    }

    /// <summary>
    /// ボタンが押されたら画面遷移する
    /// 最大数までボタンが押されたらオバケが生成される
    /// </summary>
    private async void Update()
    {
        //ボタンを揺らす(横)
        float sin = Mathf.Sin(Time.time) * width;
        startBtn.transform.position = new Vector3(startBtnPos.x + sin, startBtnPos.y, startBtnPos.z);


        // ボタンが押されたら画面遷移する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.SelectMap.WasPressedThisFrame())
        {
            //少し拡大する
            await startBtn.transform.DOScale(new Vector3(startBtn.gameObject.transform.position.x + 2.0f
                                                        , startBtn.gameObject.transform.position.y + 2.0f
                                                        , startBtn.gameObject.transform.position.z)
                                                        , 1f);

            await SceneFade.instance.SceneChange("GotoMapSelect");
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
