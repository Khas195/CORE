using System;
using UnityEngine;

public class InGame : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.InGame;
    }

    public override void OnStateEnter()
    {
        GameMaster.GetInstance().UnFreezeGame();
    }

    public override void OnStateExit()
    {
        GameMaster.GetInstance().FreezeGame();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}
