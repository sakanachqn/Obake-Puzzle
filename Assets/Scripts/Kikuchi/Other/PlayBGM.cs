using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public static bool isPlayBGM = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!isPlayBGM)
        {
            SoundManager.Instance.LoopPlay("BGM");
            isPlayBGM = true;
        }
    }

}
