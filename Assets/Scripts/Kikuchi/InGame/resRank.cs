using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resRank : MonoBehaviour
{

    [SerializeField]
    private List<Sprite> images;

    private void OnEnable()
    {
        var img = this.GetComponent<Image>();
        if(TimeCount.instance.countdownSeconds >= (TimeCount.instance.CountDownMinutes * 60) / 2)
        {
            img.sprite = images[0];
        }
        else if(TimeCount.instance.countdownSeconds > 0)
        {
            img.sprite = images[1];
        }
        else
        {
            img.sprite = images[2];
        }
    }
}
