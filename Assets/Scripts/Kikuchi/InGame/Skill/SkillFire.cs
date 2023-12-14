using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : Skill
{
    public SkillFire(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {

    }

    public override async void SkillActivate()
    {
        SkillManager.IsNowSkill = true;
        sad.ShowSkillArea("Fire");
        var pos = sad.posObj.transform.position;
        sad.HideSkillArea();
        await UniTask.Delay(1);
        foreach (KeyValuePair<ControllerManager.Direction, Vector3> kvp in sm.plCon.plMove.Directions)
        {
            if (Physics.Raycast(pos, kvp.Value, out var hit, 1))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "WoodenBox")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
