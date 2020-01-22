using System;

public class InGame : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.InGame;
    }

    public override void OnStateEnter()
    {
        string levelName = "Level" + master.GetInGameLevelIndex();
        this.master.LoadSceneAdditively(levelName);
    }

    public override void OnStateExit()
    {
        string levelName = "Level" + master.GetInGameLevelIndex();
        this.master.UnloadScene(levelName);
    }
}
