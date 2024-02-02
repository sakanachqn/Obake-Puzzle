using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class kaiten : MonoBehaviour
{
    [SerializeField]
    private float _duration = 360f;
    private void Start()
    {
        this.transform.DOLocalRotate(new Vector3(0, 360, 0), _duration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
