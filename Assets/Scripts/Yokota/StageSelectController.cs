using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    private ControllerInput inp;

    private Vector2 stickInclination;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Null
    }

    private Direction stickDirection = Direction.Null;

    private void Start()
    {
        inp = ControllerManager.instance.CtrlInput;

        inp.Enable();
    }

    private void Update()
    {
        if (inp.StageSelect.Back.WasPressedThisFrame())
        {
            // タイトルに戻る
        }

        if (inp.StageSelect.Tutorial.WasPressedThisFrame())
        {
            // チュートリアルに移る
        }

        if (inp.StageSelect.Decision.WasPressedThisFrame())
        {
            // メインゲームに移る
        }
    }
}
