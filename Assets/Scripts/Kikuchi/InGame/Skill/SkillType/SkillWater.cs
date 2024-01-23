using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SkillWater : Skill
{
    EffectInstance effectInstance;
    public SkillWater(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {
        effectInstance = sad.gameObject.GetComponent<EffectInstance>();
    }

    public override async void SkillActivate()
    {
        SkillManager.IsNowSkill = true;
        Vector3 pos = sad.posObj.transform.position;
        if(pos.y > 1)pos.y = 1;

        sad.HideSkillArea();
        SkillManager.IsNowEffect = true;
        await UniTask.Delay(1);
        if (Physics.Raycast(sad.transform.position, pos - sad.transform.position, out var hit, 1))
        {
            Debug.Log("hitName" + hit.collider.name);
            var direc = hit.transform.position - sad.transform.position;
            var objBack = hit.transform.position + direc;
            if (0 > objBack.x || 0 > objBack.z || 4 < objBack.x || 4 < objBack.z) return;
            if (Physics.Raycast(hit.transform.position, direc, out var hitTwo, 1)) return;
            effectInstance.WaterEffect(sad.gameObject.transform.position, direc);
            await UniTask.Delay(500);
            ObakeAnimation.Inctance.WaterAnimation();
            await UniTask.Delay(1000);
            await hit.transform.DOMove(hit.transform.position + direc, 1);
            await UniTask.Delay(1000);
            SkillManager.IsNowEffect = false;
            //if (Physics.Raycast(hit.transform.position, Vector3.down, out var tile, 1))
            //{
            //    if (hit.collider.tag == "Pitfall") return;
            //    await hit.transform.DOMove(hit.transform.position + Vector3.down, 1);
            //}
        }
        else
        {
            SkillManager.IsNowEffect = false;
            ObakeAnimation.Inctance.WaterMissAnimation();
        }
    }
}
