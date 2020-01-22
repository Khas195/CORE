using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LogHelper : SingletonMonobehavior<LogHelper>
{
    [Conditional("ENABLE_LOGS")]
    public void LogError(string message)
    {
        Debug.LogError(message);
    }

    [Conditional("ENABLE_LOGS")]
    public void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }
}
