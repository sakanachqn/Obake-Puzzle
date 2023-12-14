using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SkillWater : Skill
{
    public SkillWater(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {

    }

    public override async void SkillActivate()
    {
        SkillManager.IsNowSkill = true;
        Vector3 pos = sad.posObj.transform.position;


        sad.HideSkillArea();
        await UniTask.Delay(1);
        if (Physics.Raycast(sad.transform.position, pos - sad.transform.position, out var hit, 1))
        {
            await hit.transform.DOMove(hit.transform.position + (hit.transform.position -sad.transform.position), 1);
        }
    }
}
