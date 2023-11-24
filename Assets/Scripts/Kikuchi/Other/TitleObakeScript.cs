using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleObakeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 7f);
    }

    private void OnDestroy()
    {
        TitleGimmickManager.obakeCount--;
    }

}
