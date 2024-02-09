using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepCounter : MonoBehaviour
{
    public int stepCountNum = 0;

    private static StepCounter _instance;
    public static StepCounter Inctance => _instance;


    [SerializeField]
    private TextMeshProUGUI tmPro;

    private void Awake()
    {
        _instance = this;
        tmPro = GetComponent<TextMeshProUGUI>();
    }

    public void StepCount()
    {
        stepCountNum++;
        tmPro.text = stepCountNum.ToString();
    }


}
