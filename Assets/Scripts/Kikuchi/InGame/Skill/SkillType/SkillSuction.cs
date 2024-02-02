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
            if (hit.collider.tag == "IronBox")
            {
                ObakeAnimation.Inctance.SuctionAnimation();
                sm.suctionObj = hit.collider.gameObject;
                await ObjectMoveAnimation(sm.suctionObj, sm.transform.position, 0f);
                sm.suctionObj.SetActive(false);
                sm.plCon.plMove.isWalkCount = true;
            }
            else
            {
                ObakeAnimation.Inctance.SuctionMissAnimation();
            }
        }
        SkillManager.IsNowSkill = false;
    }

    public async void ReverseObject()
    {
        SkillManager.isNowSuction = true;
        var vec3 = sm.gameObject.transform.Find("Foward").transform.position;
        await UniTask.Delay(1);
        if (Physics.Raycast(sad.transform.position, sad.transform.forward, out var hit, 1))
        {
            ObakeAnimation.Inctance.SuctionMissAnimation();
        }
        else
        {
            vec3.x = Mathf.Round(vec3.x);
            vec3.z = Mathf.Round(vec3.z);
            if (vec3.x > 5 || vec3.x < 0 || vec3.z > 5 || vec3.z < 0)
            {
                ObakeAnimation.Inctance.SuctionMissAnimation();
            }
            else
            {
                ObakeAnimation.Inctance.SpittingoutAnimation();
                sm.suctionObj.transform.position = sm.gameObject.transform.position;
                sm.suctionObj.SetActive(true);
                await ObjectMoveAnimation(sm.suctionObj, vec3, 1.0f);
                sm.plCon.plMove.WalkCount = 0;
                sm.suctionObj = null;

                //se

                SkillManager.isNowSuction = false;
            }
        }
    }

    private async UniTask ObjectMoveAnimation(GameObject gobj,Vector3 endPos, float endSize)
    {
        gobj.transform.DOMove(endPos, 0.5f);
        await gobj.transform.DOScale(new Vector3(endSize, endSize, endSize), 0.5f);
        return;
    }


}
