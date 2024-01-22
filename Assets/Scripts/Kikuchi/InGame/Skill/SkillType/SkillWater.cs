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
            var direc = hit.transform.position - sad.transform.position;
            var objBack = hit.transform.position + direc;
            if (0 > objBack.x || 0 > objBack.z || 4 < objBack.x || 4 < objBack.z) return;
            if (Physics.Raycast(hit.transform.position, direc, out var hitTwo, 1)) return;
            await hit.transform.DOMove(hit.transform.position + direc, 1);
            //if (Physics.Raycast(hit.transform.position, Vector3.down, out var tile, 1))
            //{
            //    if (hit.collider.tag == "Pitfall") return;
            //    await hit.transform.DOMove(hit.transform.position + Vector3.down, 1);
            //}
        }
    }
}
