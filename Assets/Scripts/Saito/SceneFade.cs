using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



//　修正者:菊池
//　クラス名を変更
public class SceneFade : MonoBehaviour

{
    public static GameObject FadeCanvas;

    [SerializeField]
    private float fadetime = 1f;

    private void Awake()
    {
        if (FadeCanvas == null)
        {
            FadeCanvas = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    async void Start()
    {
        //await SceneChange("TestPlay");
    }

    public async UniTask SceneChange(string Scenename)
    {
        //フェードの処理
        var fadeImage = GetComponent<Image>();
        fadeImage.enabled = true;
        var c = fadeImage.color;
        c.a = 0f;//初期値
        fadeImage.color = c;

        await DOTween.ToAlpha(
            () => fadeImage.color,
            color => fadeImage.color = color,
            1.0f,//目標値
            fadetime//所要時間
            );

        await SceneManager.LoadSceneAsync(Scenename);//””のなかシーン名変更

        c.a = 1.0f;
        await DOTween.ToAlpha(
            () => fadeImage.color,
            color => fadeImage.color = color,
            0f,//目標値
            fadetime//所要時間
            );
    }

}
