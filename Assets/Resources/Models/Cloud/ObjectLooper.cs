using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLooper : MonoBehaviour
{
    public float speed = 5f;
    public float destroyPosition;
    public float newObjectPostion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x < destroyPosition)
        {
            RepositionObject();
        }
    }
    void RepositionObject()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = newObjectPostion;
        transform.position = newPosition;
    }
}
