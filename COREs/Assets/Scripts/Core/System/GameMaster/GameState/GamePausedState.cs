using System;
using UnityEngine;

public class GamePausedState : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.GamePaused;
    }

    public override void OnStateEnter()
    {
        GameMaster.GetInstance().SetGameTimeScale(0.0f);
    }

    public override void OnStateExit()
    {
        GameMaster.GetInstance().SetGameTimeScale(1.0f);
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}
