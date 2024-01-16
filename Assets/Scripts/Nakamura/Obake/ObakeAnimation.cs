using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObakeAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _obake;

    void Start()
    {
        _obake.GetComponent<Animator>();
    }

    /// <summary>
    /// 待機アニメーション
    /// </summary>
    public void IdleAnimation()
    {
        _obake.SetBool("Bool", true);
    }

    /// <summary>
    /// 歩くアニメーション
    /// </summary>
    public void WalkAnimation()
    {
        _obake.SetTrigger("Walk");
    }

    /// <summary>
    /// 炎スキルアニメーション
    /// </summary>
    public void FlameAnimation()
    {
        _obake.SetTrigger("Flame");
    }

    /// <summary>
    /// 炎スキルのミスアニメーション
    /// </summary>
    public void FlameMissAnimation()
    {
        _obake.SetTrigger("FlameMiss");
    }

    /// <summary>
    /// 水スキルアニメーション
    /// </summary>
    public void WaterAnimation()
    {
        _obake.SetTrigger("Water");
    }

    /// <summary>
    /// 水スキルのミスアニメーション
    /// </summary>
    public void WaterMissAnimation()
    {
        _obake.SetTrigger("WaterMiss");
    }

    /// <summary>
    /// 吸い込みアニメーション
    /// </summary>
    public void SuctionAnimation()
    {
        _obake.SetTrigger("Suction");
    }

    /// <summary>
    /// 吸い込みのミスアニメーション
    /// </summary>
    public void SuctionMissAnimation()
    {
        _obake.SetTrigger("SuctionMiss");
    }

    /// <summary>
    /// 吐き出しアニメーション
    /// </summary>
    public void SpittingoutAnimation()
    {
        _obake.SetTrigger("Spittingout");
    }
}
