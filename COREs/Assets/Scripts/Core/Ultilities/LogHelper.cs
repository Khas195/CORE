using System;
using System.Diagnostics;
using UnityEngine;

public class LogHelper : SingletonMonobehavior<LogHelper>
{
    [Conditional("ENABLE_LOGS")]
    public void LogError(string message, bool showLogOnInGame = false)
    {
        UnityEngine.Debug.LogError(message);
        if (showLogOnInGame)
        {
            ShowLogOnUI("ERROR:".TextMod("red", true, true) + message);
        }
    }

    [Conditional("ENABLE_LOGS")]
    public void Log(string message, bool showLogOnInGame = false)
    {
        UnityEngine.Debug.Log(message);
        if (showLogOnInGame)
        {
            ShowLogOnUI(message);
        }
    }

    [Conditional("ENABLE_LOGS")]
    public void LogWarning(string message, bool showLogOnInGame = false)
    {
        UnityEngine.Debug.LogWarning(message);
        if (showLogOnInGame)
        {
            ShowLogOnUI("WARNING:".TextMod("yellow", true, true) + message);
        }
    }
    private void ShowLogOnUI(string message)
    {
        var ingameUI = InGameLogUI.GetInstance(false);
        if (ingameUI)
        {
            ingameUI.ShowLog(message);
        }

    }

}
