using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AreaColorFade : MonoBehaviour
{
    Material mat;
    [SerializeField]
    private float fadeTime = 1;
    [SerializeField]
    private float underAlpha = 0.3f;

    private Color red = new Color(1f, 0, 0, 0.5f);
    private Color col;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        col = mat.color;

    }
    public void ColorFade()
    {
        mat.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SkillPos")
        {
            this.mat.color = red;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        this.mat.color = col;
    }



}
