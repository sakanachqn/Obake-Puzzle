using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コントローラー入力を管理するクラス
/// </summary>
public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;

    //スティックの傾き保存用変数
    private Vector2 stickInclination = Vector2.zero;

    private Vector2 dPadInclination = Vector2.zero;

    //デッドゾーン設定用変数
    [SerializeField]
    private float deadZone = 0.5f;

    public ControllerInput CtrlInput;

    /// <summary>
    /// スティックの倒れた方向
    /// </summary>
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Null
    };

    //スティックの倒れた方向保存用変数
    public Direction stickPlayerDirection = Direction.Null;

    public Direction dPadDirection = Direction.Null;

    /// <summary>
    /// L1 or R1 ボタンの押された方
    /// </summary>
    public enum LeftRight
    {
        Left,
        Right,
        Null
    };
    public LeftRight camLR = LeftRight.Null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);
        if(CtrlInput == null)
        {
            CtrlInput = new ControllerInput();
            CtrlInput.Enable();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ControllerUpdate()
    {
        if(!PlayerController.IsNowAction)CheckPlayerStickDirection();
        if(!PlayerController.IsNowAction) CheckPlayerDPadDirection();
        if(!PlayerController.IsNowAction)CheckPressCamRotateButton();
    }

    #region stick
    /// <summary>
    /// スティックの倒れた方向取得関数
    /// </summary>
    private void CheckPlayerStickDirection()
    {
        //スティックの入力取得
        stickInclination = CtrlInput.Player.Move.ReadValue<Vector2>();
        //正規化
        stickInclination = DeadZone(stickInclination);
        // 取得したスティックの方向に対応したenumに変換
        if (stickInclination == Vector2.zero) stickPlayerDirection = Direction.Null;
        else if (stickInclination.x == -1) stickPlayerDirection = Direction.Left;
        else if (stickInclination.x == 1) stickPlayerDirection = Direction.Right;
        else if (stickInclination.y == 1) stickPlayerDirection = Direction.Up;
        else if (stickInclination.y == -1) stickPlayerDirection = Direction.Down;
    }

    private void CheckPlayerDPadDirection()
    {
        dPadInclination = CtrlInput.Player.Rot.ReadValue<Vector2>();

        if (dPadInclination == Vector2.zero) dPadDirection = Direction.Null;
        else if (dPadInclination.x == -1) dPadDirection = Direction.Left;
        else if (dPadInclination.x == 1) dPadDirection = Direction.Right;
        else if (dPadInclination.y == 1) dPadDirection = Direction.Up;
        else if (dPadInclination.y == -1) dPadDirection = Direction.Down;
    }

    //デッドゾーン設定用関数
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
    #endregion

    #region L1R1
    /// <summary>
    /// L1 or R1が押されたかどうかの判定用関数
    /// </summary>
    private void CheckPressCamRotateButton()
    {
        if (CtrlInput.Rotate.CamL.WasPressedThisFrame()) camLR = LeftRight.Left;
        else if (CtrlInput.Rotate.CamR.WasPressedThisFrame()) camLR = LeftRight.Right;
        else camLR = LeftRight.Null;
    }
    #endregion
}
