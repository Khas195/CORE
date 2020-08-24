using System;
using System.Collections.Generic;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
public static class GameConsoleEvent
{
    public static string CONSOLE_SWITCH = "CONSOLE_SWITCH";
}
public class InGameLogUI : SingletonMonobehavior<InGameLogUI>, IObserver
{
    [SerializeField]
    [Required]
    Text consoleText = null;
    [SerializeField]
    [Required]
    ScrollRect consoleScrollRect = null;
    [SerializeField]
    [Required]
    InputField controlInputField = null;
    protected override void Awake()
    {
        base.Awake();
        PostOffice.GetInstance().Subscribes(this, GameConsoleEvent.CONSOLE_SWITCH);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        TurnOff();
    }
    public void ShowLog(string log)
    {
        var currentTime = System.DateTime.Now;
        consoleText.text += "\n" + "[" + currentTime.Hour + ":" + currentTime.Minute + ":" + currentTime.Second + "] - " + log + ".";

        consoleScrollRect.verticalNormalizedPosition = -1;
    }

    public void InputCommand(string command)
    {
        if (command == "")
        {
            controlInputField.DeactivateInputField();
            return;
        }
        ShowLog(command);
        ProcessCommand(command);
        controlInputField.text = "";
        controlInputField.Select();
        controlInputField.ActivateInputField();

    }

    private void ProcessCommand(string command)
    {
        var words = command.Split(' ');
        var commandSignal = words[0].ToLower();
        if (commandSignal.Equals("command") == false) return;
        var commandType = words[1].ToLower();
        if (commandType.Equals("clear"))
        {
            consoleText.text = "";
        }
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName.Equals(GameConsoleEvent.CONSOLE_SWITCH))
        {
            if (this.gameObject.activeSelf)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }

        }
    }

    private void TurnOn()
    {
        this.gameObject.SetActive(true);
    }

    private void TurnOff()
    {
        this.gameObject.SetActive(false);
    }
}
