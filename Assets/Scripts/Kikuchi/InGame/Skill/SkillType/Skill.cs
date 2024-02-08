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
