using UnityEngine;
using UnityEngine.SceneManagement;

namespace sample
{
    public class ReStart
        : MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
#if UNITY_STANDALONE_WIN
                System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
                Application.Quit();
#endif
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

    }
}