using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI stepsText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        ResultTimer();
        ResultSteps();
        ResultScore();
    }

    void Update()
    {

    }

    private void ResultTimer()
    {

    }

    private void ResultSteps()
    {
        stepsText.text = StepCounter.Inctance.stepCountNum.ToString("00");
    }

    private void ResultScore()
    {

    }
}
