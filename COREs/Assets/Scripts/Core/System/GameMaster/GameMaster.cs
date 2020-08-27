using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMaster : SingletonMonobehavior<GameMaster>, IObserver
{
    [SerializeField]
    [Required]
    BuildProfile profile = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        PostOffice.GetInstance().Subscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
    }

    void Start()
    {
        SceneLoadingManager.GetInstance().UnloadAllScenes(exception: profile.masterScene);
        SceneLoadingManager.GetInstance().LoadSceneAdditively(profile.logScene);
        RequestInstance(profile.startUpInstance);
    }
    void OnDestroy()
    {
        PostOffice.GetInstance().Unsubscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
    }


    public void RequestInstance(GameInstance newInstance)
    {
        SceneLoadingManager.GetInstance().InitiateLoadingSequenceFor(newInstance);
    }


    private void NotifyOnGameStateChange(GameState.GameStateEnum newGameState)
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameMasterEvent.GameStateChangeEvent.New_Game_State, newGameState);
        PostOffice.GetInstance().SendData(data, GameMasterEvent.ON_GAMESTATE_CHANGED);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PostOffice.GetInstance().SendData(null, GameConsoleEvent.CONSOLE_SWITCH);
        }
    }



    public void SetMouseVisibility(bool visibility)
    {
        Cursor.visible = visibility;
        Cursor.lockState = visibility ? CursorLockMode.None : CursorLockMode.Confined;
    }

    public void ExitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void SetGameTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
    }
    public void FreezeGame()
    {
        Time.timeScale = 0.0f;
    }
    public void UnFreezeGame()
    {
        Time.timeScale = 1.0f;
    }
}
