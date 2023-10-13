using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    [SerializeField]
    private GameObject mapTile;

    private GameObject[,,] allMapGimmicks = new GameObject[5, 4, 5];

    private void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++) 
            {
                allMapGimmicks[x, 0, z] =
                    Instantiate<GameObject>(mapTile, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

}
