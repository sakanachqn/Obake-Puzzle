using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EffectInstance : MonoBehaviour 
{
    private GameObject fireEffectPrefab;
    private GameObject waterEffectPrefab;
    private ParticleSystem effectParticle;

    public void FireEffect(Vector3 pos)
    {
        for (int i = 0; i < 4; i++)
        {
            fireEffectPrefab = Instantiate(Resources.Load<GameObject>("Effect/prefab/Obake_eff_Fire"),
                                            pos + new Vector3 (0, 0.25f, 0),
                                            Quaternion.identity);
            effectParticle = fireEffectPrefab.GetComponent<ParticleSystem>();
            effectParticle.startRotation3D = new Vector3(0, 90 * i * Mathf.Deg2Rad, 0);
        }
        
    }

    public void WaterEffect(Vector3 pos, Vector3 dir)
    {
        waterEffectPrefab = Instantiate(Resources.Load<GameObject>
            ("Effect/obake_water/prehab/Water_effect"),
            pos,
            Quaternion.identity);

        float y = 0;
        if (dir.x != 0) { y = 90 * dir.x; }
        if (dir.z != 0) { y = 90 * dir.z - 90; }
        waterEffectPrefab.transform.rotation = Quaternion.Euler(0, y, -80);
        }
}
