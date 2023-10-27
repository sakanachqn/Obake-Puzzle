using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoalController : MonoBehaviour
{

    [SerializeField]Image ResultImage;
    [SerializeField] float Y = 0;//Y座標
    [SerializeField] float X = 0;//X座標
    [SerializeField] float Z = 0;//Z座標
    [SerializeField] float S = 0f;//スピード

    void Start()
    {
        StartCoroutine("WaiteImage");
        ResultImage = GameObject.Find("Image").GetComponent<Image>();
        ResultImage.enabled = false;
    }

    IEnumerator WaiteImage()
    {

        //3秒停止
        yield return new WaitForSeconds(3);

        //画像表示
        ResultImage.enabled = true;

        //画像移動
        transform.DOMove(new Vector3(X,Y,Z),S);
        
    }


    





}
