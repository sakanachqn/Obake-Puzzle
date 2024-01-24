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

    private Image image;
    [SerializeField]
    private Image childImage;

    private Vector3 initialPos;

    private Tweener shakeEvent;

    private void Start()
    {
        // 自分のイメージコンポーネントを取得
        image = gameObject.GetComponent<Image>();

        transform.DOShakePosition(100f, 5f, 1, 1, false, false).SetEase(Ease.OutElastic).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        // カーソルがあっているとき
        if (matchCursor)
        {
            childImage.gameObject.SetActive(true);

            // タイムラインを進める
            timeLine += Time.deltaTime;
            
            // イメージのカラーを白にする
            image.color = Color.white;

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
            childImage.gameObject.SetActive(false);

            // タイムラインを初期化
            timeLine = 0f;

            //timeLine += Time.deltaTime;
            // イメージをグレーにする
            image.color = Color.gray;
            // 大きさを初期化
            gameObject.transform.localScale = Vector3.one;
        }
    }
}
