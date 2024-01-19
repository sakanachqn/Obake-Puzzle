using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class SkillSuction : Skill
{
    public SkillSuction(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {

    }

    public override async void SkillActivate()
    {
        SkillManager.IsNowSkill = true;

        await UniTask.Delay(1);
        if(Physics.Raycast(sad.transform.position, sad.transform.forward, out var hit, 1) && sm.suctionObj == null)
        {
            ObakeAnimation.Inctance.SuctionAnimation();
            sm.suctionObj = hit.collider.gameObject;
            sm.suctionObj.SetActive(false);
            sm.plCon.plMove.isWalkCount = true;
        }
        SkillManager.IsNowSkill = false;
    }

    public async void ReverseObject()
    {
        SkillManager.isNowSuction = true;
        Debug.Log("kitayo");
        var vec3 = sm.gameObject.transform.Find("Foward").transform.position;
        await UniTask.Delay(1);
        if (Physics.Raycast(sad.transform.position, sad.transform.forward, out var hit, 1))
        {
            ObakeAnimation.Inctance.SuctionMissAnimation();
            Debug.Log("A");
        }
        else
        {
            Debug.Log("C");
            sm.suctionObj.transform.position = vec3;
            vec3.x = Mathf.Round(vec3.x);
            vec3.z = Mathf.Round(vec3.z);
            if (vec3.x > 5 || vec3.x < 0 || vec3.z > 5 || vec3.z < 0)
            {
                ObakeAnimation.Inctance.SuctionMissAnimation();
            }
            else
            {
                ObakeAnimation.Inctance.SpittingoutAnimation();
                sm.suctionObj.SetActive(true);
                sm.plCon.plMove.WalkCount = 0;
                sm.suctionObj = null;
                SkillManager.isNowSuction = false;
            }
        }
    }


}
