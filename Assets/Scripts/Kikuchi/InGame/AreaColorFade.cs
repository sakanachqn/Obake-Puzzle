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

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

    }
    public void ColorFade()
    {
        mat.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
    }
}
