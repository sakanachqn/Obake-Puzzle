using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    void Update()
    {
        //Escが押された時
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                //ゲームプレイ終了
                UnityEditor.EditorApplication.isPlaying = false;
            #else
            //ゲームプレイ終了
                Application.Quit();
            #endif
        }
    }
}
