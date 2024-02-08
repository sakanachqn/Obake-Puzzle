using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeunUtility_Image : MonoBehaviour
{
    public void OnImage()
    {
        this.transform.Find("Obake").gameObject.SetActive(true);
    }
    
    public void OffImage()
    {
        this.transform.Find("Obake").gameObject.SetActive(false);
    }

}