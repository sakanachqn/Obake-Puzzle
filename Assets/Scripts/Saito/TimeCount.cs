using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCount : MonoBehaviour
{

    public int CountDownMinutes = 5;
    private float countdownSeconds;
    private TMP_Text timeText;

    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        countdownSeconds = CountDownMinutes * 60;
    }

    
    void Update()
    {
        countdownSeconds -= Time.deltaTime;
       TimeSpan timeSpan = TimeSpan.FromSeconds(countdownSeconds);
        timeText.text = timeSpan.ToString(@"mm\:ss");
        if(countdownSeconds <= 0)//0秒になったときの処理
        {
            //ゲームオーバーの処理
        }
    }
}
