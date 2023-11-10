using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageImageView : MonoBehaviour
{
    public bool matchCursor = false;

    private float timeLine = 0f;

    private Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (matchCursor)
        {
            timeLine += Time.deltaTime;
            
            image.color = Color.white;

            gameObject.transform.localScale
                = Vector3.one 
                + new Vector3(Mathf.Abs(Mathf.Sin(timeLine * 3) / 10)
                             , Mathf.Abs(Mathf.Sin(timeLine * 3) / 10)
                             , 0);
        }
        else
        {
            timeLine = 0f;
            image.color = Color.gray;
            gameObject.transform.localScale = Vector3.one;
        }
    }
}
