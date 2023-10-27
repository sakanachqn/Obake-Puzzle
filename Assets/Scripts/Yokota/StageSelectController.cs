using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    //デッドゾーン設定用変数
    [SerializeField]
    private float deadZone = 0.5f;

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
        stickInclination = inp.StageSelect.CursorMove.ReadValue<Vector2>();

        //正規化
        stickInclination = DeadZone(stickInclination);

        if (stickInclination == Vector2.zero) stickDirection = Direction.Null;
        else if (stickInclination.x == -1) stickDirection = Direction.Left;
        else if (stickInclination.x == 1) stickDirection = Direction.Right;
        else if (stickInclination.y == 1) stickDirection = Direction.Up;
        else if (stickInclination.y == -1) stickDirection = Direction.Down;
    }

    private Vector2 DeadZone(Vector2 vec2)
    {
        vec2.x = DeadZoneCheck(vec2.x);
        vec2.y = DeadZoneCheck(vec2.y);
        return vec2;
    }

    /// <summary>
    /// 引数が絶対値0.5以下かどうか
    /// </summary>
    /// <param name="value"></param>
    /// <returns>0.5未満なら0 / 0.5以上なら1 or -1</returns>
    private int DeadZoneCheck(float value)
    {
        bool minus = false;
        if (value < 0) minus = true;
        float abs = Mathf.Abs(value);
        if (abs <= deadZone) return 0;
        else if (minus) return -1;
        else return 1;
    }
}
