using UnityEngine;

public static class DevLog
{
#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
    [System.Diagnostics.Conditional("OBAKE-PUZZLE-DEBUG")]
#endif
    public static void Log(string msg)
    {
        Debug.Log(msg);
    }

#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
	[System.Diagnostics.Conditional("OBAKE-PUZZLE-DEBUG")]
#endif
    public static void LogWarning(string msg)
    {
        Debug.LogWarning(msg);
    }

#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
	[System.Diagnostics.Conditional("OBAKE-PUZZLE-DEBUG")]
#endif
    public static void LogError(string msg)
    {
        Debug.LogError(msg);
    }
}