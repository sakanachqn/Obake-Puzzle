using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using DG.Tweening;

/// <summary>
/// Playerの入力及び移動周り処理用クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Header("移動時間")]
    private float moveTime = 2f;
    
    [SerializeField]
    [Header("回転時間")]
    private float rotateTime = 2f;

    //正面方向取得用クラス格納用変数
    [SerializeField]
    private ObjectRotation objectRotation;
    public ObjectRotation objRotate
    {
        get => objectRotation;
    }

    //プレイヤー移動クラス
    private PlayerMove playerMove;
    public PlayerMove plMove
    {
        get => playerMove;
    }

    //プレイヤーカメラ回転クラス
    private PlayerRotate playerRotate;

    //何かしらのコルーチン中かどうかのフラグ
    public static bool isNowAction = false;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerRotate = GetComponent<PlayerRotate>();
    }


    // Start is called before the first frame update
    async void Start()
    {

        //キャンセルトークンの取得
        var token = this.GetCancellationTokenOnDestroy();

        //Dictonary Init
        var vec3s = objectRotation.SetFoward();
        playerMove.SetDirectionDictionary(vec3s[0], vec3s[1], vec3s[2], vec3s[3]);

        //Playerの移動/カメラの回転処理の開始
　       await UniTask.WhenAll(playerMove.MoveTask(token, moveTime), playerRotate.CamRotateTask(token,rotateTime));
    }

}
