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
    private float underAlpha;

    private Color red = new Color(1f, 0, 0, 0.9f);
    private Color col;

    Tweener tweener;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        col = mat.color;

    }
    public void ColorFade()
    {
        tweener = mat.DOFade(0.6f, fadeTime).SetLoops(-1, LoopType.Yoyo);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "SkillPos")
        {
            tweener.Pause();
            this.mat.color = red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.mat.color = col;
        tweener.Play();
    }



}
