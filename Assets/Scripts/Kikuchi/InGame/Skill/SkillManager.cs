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
    public bool isPosSelectNow = false;

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
                skillArea.GetSkillPos();
            }
            if(ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillA)
            {
                currentSkillA.SkillActivate();
                SoundManager.Instance.Play("SEWater");
            }
            if (ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillB)
            {
                currentSkillB.SkillActivate();
                SoundManager.Instance.Play("SEFire");
            }
        }
    }

    


}
