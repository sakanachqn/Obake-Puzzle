using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resTime : MonoBehaviour
{
    private void OnEnable()
    {
        var tmPro = GetComponent<TextMeshProUGUI>();
        tmPro.text = TimeCount.instance.countdownSeconds.ToString("F0");
    }
}
