using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.MainMenu;
    }

    public override void OnStateEnter()
    {
        LogHelper.GetInstance().Log("Main Menu Scene Enter", true);
        this.master.LoadSceneAdditively("MainMenu");
    }

    public override void OnStateExit()
    {
        LogHelper.GetInstance().Log("Main Menu Scene Exit", true);
        this.master.UnloadScene("MainMenu");
    }
}
