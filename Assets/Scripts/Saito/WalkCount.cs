using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalkCount : MonoBehaviour
{
    //UIテキスト指定
    public TMP_Text TextWalkCount;
    //表示する変数
    private int walk;

    void Start()
    {
        walk = 0;
    }

    
    void Update()
    {
        TextWalkCount.text = string.Format("{0}マス",walk);
        walk++;
    }
}
