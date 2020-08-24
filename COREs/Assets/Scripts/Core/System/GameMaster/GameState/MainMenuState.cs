﻿using System;
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
        this.master.LoadMainMenu();
    }

    public override void OnStateExit()
    {
        LogHelper.GetInstance().Log("Main Menu Scene Exit", true);
        this.master.UnloadMainMenu();
    }

    public override void UpdateState()
    {
    }
}
