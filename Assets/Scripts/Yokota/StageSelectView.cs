using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectView : MonoBehaviour
{
    // すべてのステージ選択UIを格納しておくリスト
    private List<GameObject> stageObjs = new List<GameObject>();

    // すべてのステージのキャプチャ画像を格納しておくリスト
    private List<Image> stageCaptures = new List<Image>();

    // キャプチャ画像についているStageImageViewを格納するリスト
    [SerializeField]
    private List<StageImageView> stageImageViews;

    // 生成するステージ選択UIのプレハブリスト
    [SerializeField]
    private List<GameObject> prefabImages;

    // UIを生成するキャンバス
    [SerializeField]
    private Canvas canvas;

    // ステージ数
    [SerializeField]
    private int stageNum;

    public int StageNum => stageNum;

    // UIがアニメーションしているかのフラグ
    public bool isMoving = false;

    // ステージ１のUIを生成するポジション
    private Vector3 startSetPosition = new Vector3(325f, 590f);

    // ひとつ前のステージUIからどれだけの間隔を開けて配置するか
    private float positionIntervalX = 325f;
    private float positionIntervalY = 390f;

    // ステージキャプチャ画像の素材を格納しておくディクショナリ
    private Dictionary<int, Sprite> stageSprites = new Dictionary<int, Sprite>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // ステージ数だけキャプチャ画像を読み込む
        for (int i = 1; i <= stageNum; i++)
        {
            stageSprites.Add(i, Resources.Load<Sprite>("UI/StageSelectImg/Map_00" + i));
        }

        // ステージ数だけUIを生成する
        for (int i = 0; i < stageNum; i++)
        {
            stageObjs.Add(Instantiate(prefabImages[i % 6]));

            // キャプチャ画像を貼り付けるイメージを子オブジェクトから取得する
            var children = stageObjs[i].GetComponentsInChildren<Image>();
            stageCaptures.Add(children[1]);

            stageImageViews.Add(stageObjs[i].GetComponentInChildren<StageImageView>());

            // キャンバスの子オブジェクトに設定
            stageObjs[i].transform.SetParent(canvas.transform, false);

            if (i == 0) 
            {
                // 基準となるステージ１のUIはスタートポジションに配置する
                stageObjs[i].transform.position = startSetPosition;
            }
            else if (i % 2 == 0)
            {
                // 等間隔にUIを配置していく
                stageObjs[i].transform.position 
                    = stageObjs[i - 1].transform.position 
                    + new Vector3(positionIntervalX, positionIntervalY, 0);
            }
            else
            {
                // 等間隔にUIを配置していく
                stageObjs[i].transform.position 
                    = stageObjs[i - 1].transform.position 
                    + new Vector3(positionIntervalX, -positionIntervalY, 0);
            }

            // ステージキャプチャ画像を設定
            stageCaptures[i].sprite = stageSprites[i + 1];
        }
    }

    
    /// <summary>
    /// 引数から左右にUIをアニメーションさせる関数
    /// </summary>
    /// <param name="key"></param>
    public void MoveLeftOrRight(float key)
    {
        // アニメーションフラグをあげる
        isMoving = true;

        // UIの数だけアニメーションさせる
        for (int i = 0; i < stageObjs.Count; i++)
        {
            // アニメーションが終わった時にフラグを下す
            stageObjs[i].transform.DOMoveX(key * -325f, 1f).
                SetRelative(true).
                OnComplete(() => isMoving = false) ;
        }
    }

    /// <summary>
    /// カーソルがあっているときにキャプチャ画像を明るくする関数
    /// </summary>
    /// <param name="CursorPos"></param>
    public void BrightUp(int CursorPos)
    {
        for (int i = 0; i < stageNum; i++)
        {
            if (i == CursorPos) stageImageViews[i].matchCursor = true;
            else stageImageViews[i].matchCursor = false;
        }
    }
}
