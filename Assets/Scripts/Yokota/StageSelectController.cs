using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    private ControllerInput inp;

    // 今どのステージを選んでいるか
    private int nowSelectStage = 0;

    public static int SelectedStage;

    private StageSelectView stageSelectView;

    private SceneFade sceneFade;

    // スティックの傾きを保存する変数
    private Vector2 stickInclination;

    private bool leftEndDisplayed = false;
    private bool rightEndDisplayed = false;

    private void Start()
    {
        inp = ControllerManager.instance.CtrlInput;

        stageSelectView = GetComponent<StageSelectView>();

        // 動作試験の際に適当に使ったものです。モック版のときはタイトルシーンから使っている
        // イメージからシーンフェードのスクリプトをとってきてください
        sceneFade = SceneFade.instance;

        inp.Enable();
    }

    private async void Update()
    {
        if (inp.StageSelect.Back.WasPressedThisFrame())
        {
            // タイトルに戻る
            await sceneFade.SceneChange("TitleScene");
        }
        
        if (inp.StageSelect.Tutorial.WasPressedThisFrame())
        {
            //チュートリアルに進む
            //await sceneFade.SceneChange("")
        }

        if (inp.StageSelect.Decision.WasPressedThisFrame())
        {
            // メインゲームに進む
            SoundManager.Instance.Play("Select");
            SelectedStage = nowSelectStage + 1;
            await sceneFade.SceneChange("GameScene");
        }

        stageSelectView.BrightUp(nowSelectStage);
    }

    /// <summary>
    /// スティックが倒されたときに読まれる関数
    /// </summary>
    /// <param name="context"></param>
    public void OnNavigate(InputAction.CallbackContext context)
    {
        // performedコールバックだけをチェックする
        if (!context.performed) return;

        if (stageSelectView.isMoving) return;

        // スティックの2軸入力取得
        stickInclination = context.ReadValue<Vector2>();

        

        // スティックが左に傾いていて、カーソルが左端にないとき
        if (stickInclination.x == -1 && nowSelectStage > 0)
        {
            if (nowSelectStage <= 2) leftEndDisplayed = true;
            else leftEndDisplayed = false;

            if (nowSelectStage > stageSelectView.StageNum - 3) rightEndDisplayed = true;
            else rightEndDisplayed = false;

            if (!rightEndDisplayed && !leftEndDisplayed)
            {
                stageSelectView.MoveLeftOrRight(stickInclination.x);
            }
            // 今選ぼうとしているステージ番号を更新する
            nowSelectStage -= 1;
        }
        // スティックが右に傾いていて、カーソルが右端にないとき
        if (stickInclination.x == 1 && nowSelectStage < stageSelectView.StageNum - 1)
        {
            if (nowSelectStage < 2) leftEndDisplayed = true;
            else leftEndDisplayed = false;

            if (nowSelectStage >= stageSelectView.StageNum - 3) rightEndDisplayed = true;
            else rightEndDisplayed = false;

            if (!rightEndDisplayed && !leftEndDisplayed)
            {
                stageSelectView.MoveLeftOrRight(stickInclination.x);
            }
            // 今選ぼうとしているステージ番号を更新する
            nowSelectStage += 1;
        }
    }
}
