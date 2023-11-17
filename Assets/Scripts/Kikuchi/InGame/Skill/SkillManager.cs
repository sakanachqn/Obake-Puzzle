using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // スキル発動時の範囲を示すTransform
    [SerializeField] private Transform fireArea;

    // スキル範囲のフェード用のオブジェクト
    [SerializeField] private GameObject areaFade;

    // スキル範囲のフェード位置
    [SerializeField] private float areaFadePos = 0.505f;

    // コントローラーマネージャー
    private ControllerManager ctrl;
    
    //プレイヤーコントローラー
    private PlayerController plCon;

    // スキル範囲のTransformリスト
    private List<Transform> fireSkillArea = new List<Transform>();

    // スキル範囲のオブジェクトとそのマテリアルの辞書
    private Dictionary<GameObject, Material> hitArea = new Dictionary<GameObject, Material>();

    // 現在スキルが発動中かどうかのフラグ
    private bool isNowSkill = false;

    // デフォルトのマテリアル
    private Material mat;

    // スキル範囲のフェード用の新しいマテリアル
    [SerializeField] private Material changeMat;

    // スキル範囲のフェード時間
    [SerializeField] private float fadeTime = 1;

    // スキル範囲の下部の透明度
    [SerializeField] private float underAlpha = 0.3f;

    // スキル範囲のプレハブ
    [SerializeField] private GameObject skillAreaPrefab;

    // スキル発動位置のプレハブ
    [SerializeField] private GameObject skillPosPrefab;

    // スキルのタイプ
    private enum skillType
    {
        skillA,
        skillB,
        Null
    };

    // キャンセル用のトークン
    CancellationTokenSource cancelToken;

    // 押されたスキルの種類
    private skillType pressBtn = skillType.Null;

    // スキルの種類ごとの処理を定義するデリゲート
    private delegate void SkillTypeProcess();

    private void Start()
    {
        // コントローラーマネージャーの取得
        ctrl = ControllerManager.instance;
        //プレイヤーコントローラーの取得
        plCon = GetComponent<PlayerController>();

        // スキル範囲のTransformをリストに追加
        var index = fireArea.childCount;
        for (int i = 0; i < index; i++)
        {
            fireSkillArea.Add(fireArea.GetChild(i));
        }

        // キャンセル用のトークンの初期化
        cancelToken = new CancellationTokenSource();
    }

    /// <summary>
    /// スキル発動ボタンが押されたときに呼ばれるメソッド
    /// </summary>
    public void Skill()
    {
        // 押されたスキルボタンに対応するスキルが存在し、かつ現在スキルが発動中でない場合
        if (PressSkillButton() && !isNowSkill)
        {
            isNowSkill = true;

            // 押されたスキルボタンに応じて処理を分岐
            switch (pressBtn)
            {
                case skillType.skillA:
                    // スキルAの処理
                    break;
                //case skillType.skillB:
                //    // スキルBの処理
                //    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// スキルボタンが押されたかどうかを判定するメソッド
    /// </summary>
    /// <returns></returns>
    private bool PressSkillButton()
    {
        // スキルAボタンが押された場合
        if (ctrl.CtrlInput.Skill.SkillA.WasPressedThisFrame())
        {
            pressBtn = skillType.skillA;
            return true;
        }
        // スキルBボタンが押された場合
        else if (ctrl.CtrlInput.Skill.SkillB.WasPressedThisFrame())
        {
            pressBtn = skillType.skillB;
            return true;
        }
        // どのボタンも押されていない場合
        else
        {
            return false;
        }
    }

    /// <summary>
    /// スキル発動時の非同期処理を行うメソッド
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask SkillProcess(CancellationToken token)
    {
        // スキル範囲の表示
        ViewSkillArea();

        // プレイヤーの移動を無効化
        ctrl.CtrlInput.Player.Move.Disable();
    }

    /// <summary>
    /// スキルキャンセル時の非同期処理を行うメソッド
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask SkillCancel(CancellationToken token)
    {
        // キャンセルボタンが押されるまで待機
        await UniTask.WaitUntil(() => ctrl.CtrlInput.Player.Cancel.WasPressedThisFrame(), cancellationToken: token);

        // スキル範囲の破棄
        DestroySkillArea();

        // トークンのキャンセル
        cancelToken.Cancel();
    }

    /// <summary>
    /// スキル範囲の表示を行うメソッド
    /// </summary>
    private void ViewSkillArea()
    {
        // スキル範囲をまとめる親のオブジェクトを作成
        var parent = new GameObject("Area");

        foreach (Transform t in fireSkillArea)
        {
            // スキル範囲の下にRayを飛ばして、地面にヒットしたらスキル範囲を表示
            if (Physics.Raycast(t.position, Vector3.down, out var hit, Mathf.Infinity))
            {
                // "Pitfall"タグのオブジェクトにヒットした場合はスキップ
                if (hit.collider.tag == "Pitfall")
                    return;

                // 地面にヒットした場合、スキル範囲を生成
                if (hit.transform.position.y != 0)
                {
                    Vector3 pos = hit.transform.position;
                    var temp = Instantiate(skillAreaPrefab, pos, Quaternion.identity, parent.transform);
                    var tempMat = temp.GetComponent<MeshRenderer>();
                    hitArea.Add(temp, mat);
                    tempMat.material = changeMat;
                    tempMat.material.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
                }

                // 地面にヒットした場合、上にずらして生成
                if (hit.transform.position.y == 0)
                {
                    Vector3 pos = hit.transform.position;
                    pos.y += 1;
                    var temp = Instantiate(skillAreaPrefab, pos, Quaternion.identity, parent.transform);
                    var tempMat = temp.GetComponent<MeshRenderer>();
                    hitArea.Add(temp, tempMat.material);
                    tempMat.material = changeMat;
                    tempMat.material.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
                }
            }
        }
    }

    /// <summary>
    /// スキル発動位置を取得する非同期メソッド
    /// </summary>
    /// <returns></returns>
    private async UniTask GetSkillPos()
    {
        // プレイヤーの現在位置を取得
        var startPos = this.transform.position;

        // スキル発動位置のオブジェクトを生成
        var posObj = Instantiate(skillPosPrefab, startPos, Quaternion.identity);
        var pos = posObj.transform.position;
        

        // 方向が確定するまでループ
        while (true)
        {
            // コントローラーマネージャーのstickSkillDirectionがNullでない場合
            await UniTask.WaitUntil(() => ControllerManager.instance.stickSkillDirection != ControllerManager.Direction.Null);
            var direcDic = plCon.plMove.Directions;

            // 方向に応じた処理を実行
            switch (ControllerManager.instance.stickSkillDirection)
            {
                case ControllerManager.Direction.Left:
                    // 左に移動する処理
                    pos += direcDic[ControllerManager.Direction.Left];
                    break;
                case ControllerManager.Direction.Right:
                    // 右に移動する処理
                    pos += direcDic[ControllerManager.Direction.Right];
                    break;
                case ControllerManager.Direction.Up:
                    // 上に移動する処理
                    pos += direcDic[ControllerManager.Direction.Up];
                    break;
                case ControllerManager.Direction.Down:
                    // 下に移動する処理
                    pos += direcDic[ControllerManager.Direction.Down];
                    break;
            }
        }
    }

    /// <summary>
    /// スキル範囲を破棄するメソッド
    /// </summary>
    private void DestroySkillArea()
    {
        // スキル範囲のオブジェクトと元のマテリアルに戻す
        foreach (KeyValuePair<GameObject, Material> kvp in hitArea)
        {
            kvp.Key.GetComponent<MeshRenderer>().material = kvp.Value;

            // タグが"clone"の場合はオブジェクトを破棄
            if (kvp.Key.tag == "clone")
                Destroy(kvp.Key.gameObject);
        }

        // 辞書をクリアし、スキル発動中のフラグをオフにしてプレイヤーの移動を有効にする
        hitArea.Clear();
        isNowSkill = false;
        ctrl.CtrlInput.Player.Move.Enable();
    }

    /// <summary>
    /// 移動先にobjectがあるか確認するメソッド
    /// </summary>
    /// <param name="targetDirec">スティックの方角</param>
    /// <returns>移動先にobjがあったらtrue</returns>
    private bool CheckObject(Vector3 startPos, Vector3 targetDirec, out string name)
    {
        //移動したい方向にrayを飛ばしてオブジェクトがあるか確認
        if (Physics.Raycast(startPos, targetDirec, out var hitObj, 1))
        {
            if (hitObj.collider.tag == "Pitfall")
            {
                name = null;
                return false;
            }
            name = hitObj.collider.gameObject.name;
            return true;
        }
        else
        {
            name = null;
            return false;
        }
    }

    /// <summary>
    /// マップ外かどうかのチェック
    /// </summary>
    /// <param name="targetDirec">スティックの方角</param>
    /// <returns>オブジェクトがなかったらtrue</returns>
    private bool CheckOffMap(Vector3 targetDirec)
    {
        Vector3 startPos = this.transform.position + targetDirec;
        //移動先から真下にray飛ばして地面があるか確認
        if (CheckObject(startPos, Vector3.down, out var name))
        {
            return false;
        }
        else return true;
    }
}
