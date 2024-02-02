using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField]
    private float jumpPower = 100f;

    private Vector3 startBtnPos;

    [SerializeField]
    private GameObject bowl;

    [SerializeField]
    private List<Material> bowlMaterials = new List<Material>();

    Tweener startButtonTween;

    private void Start()
    {
        //スタートボタンの位置を保存
        startBtnPos = startBtn.rectTransform.localPosition;

        //受け皿のマテリアルをランダムに変更
        int index = Random.Range(0,bowlMaterials.Count);
        bowl.GetComponent<MeshRenderer>().material = bowlMaterials[index];

        //ボタンを揺らす(ループ)
        var rect = startBtn.GetComponent<RectTransform>();
        //rect.DOJumpAnchorPos(new Vector2(width, startBtnPos.y), jumpPower, 1, 1,true).SetLoops(-1, LoopType.Yoyo);
        startButtonTween = rect.DOScale(0.7f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// ボタンが押されたら画面遷移する
    /// 最大数までボタンが押されたらオバケが生成される
    /// </summary>
    private async void Update()
    {
        // ボタンが押されたら画面遷移する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.Start.WasPressedThisFrame())
        {
            startButtonTween.Kill();
            SoundManager.Instance.Play("Select");
            //少し拡大する
            await startBtn.transform.DOScale(0.5f, 0.5f);

            await SceneFade.instance.SceneChange("StageSelect");
        }

        //オバケが最大数超えたら
        if (obakeCount > maxObakeCount) return;

        //ボタン押されたらオバケを生成する
        if (ControllerManager.instance.CtrlInput.TitleGimmick.Obake.WasPressedThisFrame())
        {
            //ランダム位置
            obakePosition = new Vector3(4.0f, Random.Range(2.6f, 5.0f), -0.86f);

            Instantiate(obake, obakePosition, Quaternion.Euler(0, 180, 0));
            obakeCount++;
        }
    }
}
