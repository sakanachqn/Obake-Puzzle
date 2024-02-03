using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageImageView : MonoBehaviour
{
    // 今このイメージにカーソルがあっているか判定する変数
    public bool matchCursor = false;

    // タイムライン
    private float timeLine = 0f;

    // ステージのキャプチャ画像
    private Image StageCapture;

    // カーソルの画像
    [SerializeField]
    private Image cursor;

    private void Start()
    {
        // 自分のイメージコンポーネントを取得
        StageCapture = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        // カーソルがあっているとき
        if (matchCursor)
        {
            // タイムラインを進める
            timeLine += Time.deltaTime;

            // カーソルをアクティブに
            cursor.gameObject.SetActive(true);

            // イメージのカラーを白にする
            StageCapture.color = Color.white;

            // スケールを周期的に拡縮する
            gameObject.transform.localScale
                = Vector3.one 
                + new Vector3(Mathf.Abs(Mathf.Sin(timeLine * 3) / 10)
                             , Mathf.Abs(Mathf.Sin(timeLine * 3) / 10)
                             , 0);
        }
        // カーソルがあっていないとき
        else
        {
            // カーソルを非アクティブに
            cursor.gameObject.SetActive(false);

            // タイムラインを初期化
            timeLine = 0f;

            //timeLine += Time.deltaTime;
            // イメージをグレーにする
            StageCapture.color = Color.gray;
            // 大きさを初期化
            gameObject.transform.localScale = Vector3.one;
        }
    }
}
