using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : Skill
{

    private EffectInstance effectInstance;
    public SkillFire(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {
        effectInstance = sad.gameObject.GetComponent<EffectInstance>();
    }

    public override async void SkillActivate()
    {
        SkillManager.IsNowSkill = true;
        var pos = sad.posObj.transform.position;
        if(pos.y > 1) pos.y = 1;
        sad.HideSkillArea();
        SkillManager.IsNowEffect = true;
        await UniTask.Delay(1);

        var boxs = new List<GameObject>();
        foreach (KeyValuePair<ControllerManager.Direction, Vector3> kvp in sm.plCon.plMove.Directions)
        {
            if (Physics.Raycast(pos, kvp.Value, out var hit, 1))
            {
                if (hit.collider.tag == "WoodenBox")
                {
                    boxs.Add(hit.collider.gameObject);
                }
            }
        }
        if(boxs.Count > 0)
        {
            SoundManager.Instance.Play("SEFire");
            ObakeAnimation.Inctance.FlameAnimation();
            await UniTask.Delay(100);
            effectInstance.FireEffect(pos);
            await UniTask.Delay(1000);
            foreach (GameObject box in boxs)
            {
                Destroy(box);
            }
            await UniTask.Delay(500);
            SkillManager.IsNowEffect = false;
        }
        else
        {
            ObakeAnimation.Inctance.FlameMissAnimation();
            SkillManager.IsNowEffect = false;
        }
    }
}


