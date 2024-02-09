using UnityEngine;

public class Yrot : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 10 * Time.deltaTime, 0);
    }
}
