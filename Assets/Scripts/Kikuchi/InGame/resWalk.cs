using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resWalk : MonoBehaviour
{
    private void OnEnable()
    {
        var tmPro = GetComponent<TextMeshProUGUI>();
        tmPro.text = StepCounter.Inctance.stepCountNum.ToString();
    }
}
