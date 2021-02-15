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
    BuildProfile requiredScenes = null;
    [SerializeField]
    [Required]
    SceneLoadingManager loadingManager = null;
    [SerializeField]
    [Required]
    GameInstance startInstance = null;


    public void StartGame()
    {
        this.RequestInstance(startInstance);
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        PostOffice.Subscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
    }

    void Start()
    {
        loadingManager.UnloadAllScenes(exception: requiredScenes.masterScene);
        loadingManager.LoadSceneAdditively(requiredScenes.logScene);
        RequestInstance(requiredScenes.startUpInstance);
    }
    void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
    }


    public void RequestInstance(GameInstance newInstance)
    {
        loadingManager.InitiateLoadingSequenceFor(newInstance);
    }


    private void NotifyOnGameStateChange(GameState.GameStateEnum newGameState)
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameMasterEvent.GameStateChangeEvent.New_Game_State, newGameState);
        PostOffice.SendData(data, GameMasterEvent.ON_GAMESTATE_CHANGED);
        DataPool.GetInstance().ReturnInstance(data);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PostOffice.SendData(null, GameMasterEvent.InGameLogConsoleEvent.CONSOLE_SWITCH_ON_OFF);
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
