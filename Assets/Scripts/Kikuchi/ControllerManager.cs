using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コントローラー入力を管理するクラス
/// </summary>
public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;

    public ControllerInput CtrlInput;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(CtrlInput == null)
        {
            CtrlInput = new ControllerInput();
            CtrlInput.Enable();
        }
    }
}
