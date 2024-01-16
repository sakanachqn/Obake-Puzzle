using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class kaiten : MonoBehaviour
{
    private void Start()
    {
        this.transform.DORotate(new Vector3(0, 360, 0), 120, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
