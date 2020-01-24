using System;
using System.Diagnostics;

public class LogHelper : SingletonMonobehavior<LogHelper>
{
    [Conditional("ENABLE_LOGS")]
    public void LogError(string message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [Conditional("ENABLE_LOGS")]
    public void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }

    [Conditional("ENABLE_LOGS")]
    public void LogWarning(string message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    [Conditional("ENABLE_LOGS")]
    public void LogBars()
    {
        UnityEngine.Debug.Log("===========================================================================");
    }
}
