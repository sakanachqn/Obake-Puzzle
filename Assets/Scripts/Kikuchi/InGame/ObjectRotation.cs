using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mapの回転に対して正面を取得するクラス
/// </summary>
public class ObjectRotation : MonoBehaviour
{
    private GameObject fowardPov;

    // Start is called before the first frame update
    private void Awake()
    {
        fowardPov = this.gameObject;
    }

    /// <summary>
    /// 今の向きに対しての各方向を取得
    /// </summary>
    /// <returns>Vec3型　前　後ろ　右　左の順番の配列</returns>
    public Vector3[] SetFoward()
    {
        Vector3 cameraForward = Vector3.Scale(fowardPov.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraBack = -cameraForward;
        Vector3 cameraRight = Vector3.Scale(fowardPov.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraLeft = -cameraRight;
        Vector3[] vector3s = new Vector3[4] { cameraForward, cameraBack, cameraLeft, cameraRight};
        return vector3s;
    }


}
