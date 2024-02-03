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
        waterEffectPrefab = Instantiate(Resources.Load<GameObject>("Effect/prefab/Water_effect"),
            pos, 
            this.transform.root.transform.rotation);
        effectParticle = waterEffectPrefab.GetComponent<ParticleSystem>();
        float z = 0;
        if (dir.x != 0) { z = 90 * dir.x * Mathf.Deg2Rad; }
        if (dir.z != 0) { z = (90 * dir.z - 90) * Mathf.Deg2Rad; }
        effectParticle.startRotation3D = new Vector3(0, 0, z);
        Destroy(waterEffectPrefab, 3);
    }
}
