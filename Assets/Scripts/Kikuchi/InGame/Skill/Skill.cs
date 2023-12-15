using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour 
{

    protected SkillAreaDisplay sad;
    protected SkillManager sm;
    public Skill(SkillAreaDisplay skillAreaDisplay, SkillManager skillmanager)
    {
        this.sad = skillAreaDisplay;
        this.sm = skillmanager;
    }

    public abstract void SkillActivate();


}
