using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCount : MonoBehaviour
{
    public static TimeCount instance;

    public int CountDownMinutes = 5;
    public float countdownSeconds;
    private TMP_Text timeText;

    public bool IsTimerStop = false;

    void Start()
    {
        instance = this;
        timeText = GetComponent<TMP_Text>();
        countdownSeconds = CountDownMinutes * 60;
    }

    
    void Update()
    {
        if (IsTimerStop) return;

        countdownSeconds -= Time.deltaTime;
       TimeSpan timeSpan = TimeSpan.FromSeconds(countdownSeconds);
        timeText.text = timeSpan.ToString(@"mm\:ss");
        if(countdownSeconds <= 0)//0秒になったときの処理
        {
           IsTimerStop = true;
        }
    }
}
