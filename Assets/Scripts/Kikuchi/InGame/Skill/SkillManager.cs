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
    // コントローラーマネージャー
    public ControllerManager ctrl;
    
    //プレイヤーコントローラー
    public PlayerController plCon;

    // スキル範囲表示
    private SkillAreaDisplay skillArea;

    // 現在スキルが発動中かどうかのフラグ
    public static bool IsNowSkill = false;

    // 現在スキル発動位置選択中かどうか
    private bool isPosSelectNow = false;

    // 各スキルのクラスを用意
    private Skill currentSkillA;
    private Skill currentSkillB;

    // スキルのタイプ
    private enum skillType
    {
        skillA,
        skillB,
        Null
    };
    // 押されたボタンの種類判別用
    private skillType selectSkill = skillType.Null;

    public void SkillManagerStart()
    {
        // コントローラーマネージャーの取得
        ctrl = ControllerManager.instance;
        // プレイヤーコントローラーの取得
        plCon = GetComponent<PlayerController>();
        // スキル範囲表示クラス取得
        skillArea = GetComponent<SkillAreaDisplay>();
        // 各スキルクラスを変数に格納
        currentSkillA = new SkillWater(skillArea, this);
        currentSkillB = new SkillFire(skillArea, this);


    }


    public void SkillManagerUpdate()
    {
        if (ctrl.CtrlInput.Skill.SkillA.WasPressedThisFrame())
        {
            if(!IsNowSkill)
            {
                IsNowSkill = true;
                skillArea.ShowSkillArea();
                selectSkill = skillType.skillA;
            }
        }
        if (ctrl.CtrlInput.Skill.SkillB.WasPressedThisFrame())
        {
            if (!IsNowSkill)
            {
                IsNowSkill = true;
                skillArea.ShowSkillArea(true);
                selectSkill = skillType.skillB;
            }
        }
        if (IsNowSkill)
        {
            SkillProcess();
        }

    }



    private void SkillProcess()
    {
        if (skillArea.areaViewNow)
        {
            if (ctrl.CtrlInput.Skill.Cancel.WasPressedThisFrame()) skillArea.HideSkillArea();
            // コントローラーマネージャーのstickSkillDirectionがNullでない場合
            if (ControllerManager.instance.stickPlayerDirection != ControllerManager.Direction.Null && !isPosSelectNow)
            {
                GetSkillPos();
            }
            if(ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillA)
            {
                currentSkillA.SkillActivate();
            }
            if (ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillB)
            {
                currentSkillB.SkillActivate();
            }
        }
    }

    /// <summary>
    /// スキル発動位置を取得する非同期メソッド
    /// </summary>
    /// <returns></returns>
    private async void GetSkillPos()
    {
        isPosSelectNow = true;

        var direcDic = plCon.plMove.Directions;

        var pos = skillArea.posObj.transform.position;

        // 方向に応じた処理を実行
        if(CheckObject(pos, direcDic[ControllerManager.instance.stickPlayerDirection], out string name))
        {
            skillArea.posObj.transform.position += direcDic[ControllerManager.instance.stickPlayerDirection];
            if(name == "Player")
            {
                skillArea.posObj.transform.position += direcDic[ControllerManager.instance.stickPlayerDirection];
            }
            if(name == "Up")
            {
                int layermask = 1 << 7;
                if (Physics.Raycast(skillArea.posObj.transform.position, Vector3.up, out var hit, 3, layermask))
                {
                    skillArea.posObj.transform.position = hit.transform.position;
                }
            }
        }
        else 
        {
            if (!CheckOffMap(direcDic[ControllerManager.instance.stickPlayerDirection]))
            {
                skillArea.posObj.transform.position = this.transform.position + direcDic[ControllerManager.instance.stickPlayerDirection];
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
                int layermask = 1 << 7;
                if(Physics.Raycast(hitObj.transform.position, Vector3.up, 5, layermask))
                {
                    name = "Up";
                    return true;
                }
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
        Vector3 startPos = skillArea.posObj.transform.position + targetDirec;
        //移動先から真下にray飛ばして地面があるか確認
        if (CheckObject(startPos, Vector3.down, out var name))
        {
            return true;
        }
        else return false; ;
    }

}
