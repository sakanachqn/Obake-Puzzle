using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    private ControllerInput inp;

    // 今どのステージを選んでいるか
    private int nowSelectStage = 0;

    // 今どのイメージににカーソルがあっているか
    private int nowCursorPos = 0;

    private StageSelectView stageSelectView;

    private SceneFade sceneFade;

    // スティックの傾きを保存する変数
    private Vector2 stickInclination;

    private bool callOneTime;

    private void Start()
    {
        inp = ControllerManager.instance.CtrlInput;

        stageSelectView = GameObject.Find("BackGround").GetComponent<StageSelectView>();

        // 動作試験の際に適当に使ったものです。モック版のときはタイトルシーンから使っている
        // イメージからシーンフェードのスクリプトをとってきてください
        sceneFade = SceneFade.instance;

        inp.Enable();

        callOneTime = false;
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
            await sceneFade.SceneChange("TestTutorial");
        }

        if (inp.StageSelect.Decision.WasPressedThisFrame() ||
            Input.GetKeyDown(KeyCode.K))
        {
            if (!callOneTime)
            {
                callOneTime = true;

                // メインゲームに進む
                SceneManager.sceneLoaded += GameSceneLoaded;

                await sceneFade.SceneChange("GameScene");
            }
        }

        stageSelectView.BrightUp(nowCursorPos);
    }

    /// <summary>
    /// スティックが倒されたときに読まれる関数
    /// </summary>
    /// <param name="context"></param>
    public void OnNavigate(InputAction.CallbackContext context)
    {
        // performedコールバックだけをチェックする
        if (!context.performed) return;

        // スティックの2軸入力取得
        stickInclination = context.ReadValue<Vector2>();

        // スティックが左に傾いているとき
        if (stickInclination.x == -1 || Input.GetKeyDown(KeyCode.A))
        {
            if (nowCursorPos < 2 && stageSelectView.PageDown())
            {
                nowCursorPos = (nowCursorPos % 2) + 4;
                // 今選ぼうとしているステージ番号を更新する
                nowSelectStage -= 2;
            }
            else if (nowSelectStage > 1)
            {
                // カーソルの位置を左へ動かす
                nowCursorPos -= 2;
                // 今選ぼうとしているステージ番号を更新する
                nowSelectStage -= 2;
            }
            else { }
        }
        // スティックが右に傾いているとき
        if (stickInclination.x == 1 || Input.GetKeyDown(KeyCode.D))
        {
            if (nowCursorPos > 3 && stageSelectView.PageUp())
            {
                nowCursorPos = nowCursorPos % 2;
                // 今選ぼうとしているステージ番号を更新する
                nowSelectStage += 2;
            }
            else if (stageSelectView.StageNum - nowSelectStage > 2)
            {
                // カーソルの位置を右へ動かす
                nowCursorPos += 2;
                // 今選ぼうとしているステージ番号を更新する
                nowSelectStage += 2;
            }
            else { }
        }
        // スティックが上に傾いていて、カーソルが上側にないとき
        if ((stickInclination.y == 1 && nowCursorPos % 2 != 0) ||
            (Input.GetKeyDown(KeyCode.W) && nowCursorPos % 2 != 0))
        {
            // カーソルの位置を上へ動かす
            nowCursorPos--;
            // 今選ぼうとしているステージ番号を更新する
            nowSelectStage--;
        }
        // スティックが下に傾いていて、カーソルが上側にあるとき
        if ((stickInclination.y == -1 && nowCursorPos % 2 == 0) ||
            (Input.GetKeyDown(KeyCode.S) && nowCursorPos % 2 == 0))
        {
            // カーソルの位置を上へ動かす
            nowCursorPos++;
            // 今選ぼうとしているステージ番号を更新する
            nowSelectStage++;
        }
    }

    /// <summary>
    /// ゲームシーンへ遷移するときに今何番目のステージを選択したか知らせる関数
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // マップを生成するスクリプトを探す
        var mapGenerater = GameObject.FindWithTag("MapGenerater").GetComponent<CSVMapGenerate>();

        // 読み込むステージの番号を書き換える
        mapGenerater.LoadStageNum = nowSelectStage + 1;

        // この処理を削除する
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
