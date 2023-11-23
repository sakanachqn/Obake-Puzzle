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

    // コントローラーマネージャー
    private ControllerManager ctrl;
    
    //プレイヤーコントローラー
    private PlayerController plCon;

    // スキル範囲のTransformリスト
    private List<Transform> fireSkillArea = new List<Transform>();

    // スキル範囲のオブジェクトとそのマテリアルの辞書
    private Dictionary<GameObject, Material> hitArea = new Dictionary<GameObject, Material>();

    // スキル範囲内のオブジェクト情報用リスト
    private List<GameObject> hitObj = new List<GameObject>();

    // 現在スキルが発動中かどうかのフラグ
    public static bool IsNowSkill = false;

    private bool areaViewNow = false;
    private bool isPosSelectNow = false;

    private GameObject posObj = null;   
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


    // 押されたスキルの種類
    //private skillType pressBtn = skillType.Null;

    // スキルの種類ごとの処理を定義するデリゲート
    private delegate void SkillTypeProcess();

    public void SkillManagerStart()
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

    }

    public void SkillManagerUpdate()
    {
        if (ctrl.CtrlInput.Skill.SkillA.WasPressedThisFrame() && !IsNowSkill)
        {
            IsNowSkill = true;
        }
        if(IsNowSkill)
        {
            SkillProcess();
        }
    }



    private void SkillProcess()
    {
        if(!areaViewNow)ViewSkillArea();
        if (areaViewNow)
        {
            if (ctrl.CtrlInput.Skill.Cancel.WasPressedThisFrame()) DestroySkillArea();
            // コントローラーマネージャーのstickSkillDirectionがNullでない場合
            if (ControllerManager.instance.stickPlayerDirection != ControllerManager.Direction.Null && !isPosSelectNow)
            {
                GetSkillPos();
            }
            if(ctrl.CtrlInput.Skill.Select.WasPressedThisFrame())
            {
                ActiveSkill();
            }
        }
    }

    private void ActiveSkill()
    {
        var pos = posObj.transform.position;
        DestroySkillArea();
        foreach(KeyValuePair<ControllerManager.Direction, Vector3> kvp in plCon.plMove.Directions)
        {
            if(Physics.Raycast(pos, kvp.Value, out var hit, 1))
            {
                if(hit.collider.tag == "WoodenBox")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
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

                // オブジェクトにヒットした場合、スキル範囲を生成
                if (hit.transform.position.y != 0)
                {
                    hitObj.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
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
        areaViewNow = true;
        // スキル発動位置のオブジェクトを生成
        if (posObj == null)
        {
            posObj = Instantiate(skillPosPrefab, this.transform.position + plCon.plMove.Directions[ControllerManager.Direction.Up], Quaternion.identity);
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

        foreach (var obj in hitObj)
        {
            obj.SetActive(true);
        }

        // 辞書をクリアし、スキル発動中のフラグをオフにしてプレイヤーの移動を有効にする
        hitObj.Clear();
        hitArea.Clear();
        IsNowSkill = false;
        areaViewNow = false;
        if (posObj != null)
        {
            Destroy(posObj);
            posObj = null;
        }
        ctrl.CtrlInput.Player.Move.Enable();
    }

    /// <summary>
    /// スキル発動位置を取得する非同期メソッド
    /// </summary>
    /// <returns></returns>
    private async void GetSkillPos()
    {
        isPosSelectNow = true;

        var direcDic = plCon.plMove.Directions;

        var pos = posObj.transform.position;

        // 方向に応じた処理を実行
        if(CheckObject(pos, direcDic[ControllerManager.instance.stickPlayerDirection], out string name))
        {
           posObj.transform.position += direcDic[ControllerManager.instance.stickPlayerDirection];
            if(name == "Player")
            {
                posObj.transform.position += direcDic[ControllerManager.instance.stickPlayerDirection];
            }
        }
        else 
        {
            if (!CheckOffMap(direcDic[ControllerManager.instance.stickPlayerDirection]))
            {
                posObj.transform.position = this.transform.position + direcDic[ControllerManager.instance.stickPlayerDirection];
            }
        }
        await UniTask.Delay(250);
        isPosSelectNow = false;
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
            else if (hitObj.collider.tag == "clone")
            {
                name = null;
                return true;
            }
            else if (hitObj.collider.tag == "Player")
            {
                name = hitObj.collider.tag;
                return true;
            }
            else
            {
                name = null;
                return false;
            }
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
    /// <returns>オブジェクトがなかったらfalse</returns>
    private bool CheckOffMap(Vector3 targetDirec)
    {
        Vector3 startPos = posObj.transform.position + targetDirec;
        //移動先から真下にray飛ばして地面があるか確認
        if (CheckObject(startPos, Vector3.down, out var name))
        {
            return true;
        }
        else return false; ;
    }

}
