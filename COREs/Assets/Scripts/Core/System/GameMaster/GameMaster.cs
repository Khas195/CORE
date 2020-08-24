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
    StateManager gameStateManager = null;
    [SerializeField]
    [Required]
    BuildProfile profile = null;

    public void LoadMainMenu()
    {
        LoadSceneAdditively(profile.menuScenes[0]);
    }

    public void UnloadMainMenu()
    {
        UnloadScene(profile.menuScenes[0]);
    }

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
        UnloadAllScenes();
        LoadScenesList(profile.setupScenes);


        if (profile.skipMainMenu)
        {
            LoadLevel(profile.levels[0]);
        }
        else
        {
            GoToMainMenu();
        }
    }

    private void NotifyOnGameStateChange(GameState.GameStateEnum newGameState)
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameMasterEvent.GameStateChangeEvent.New_Game_State, newGameState);
        PostOffice.GetInstance().SendData(data, GameMasterEvent.ON_GAMESTATE_CHANGED);
    }

    public void UnloadAllScenes()
    {
        int numOfScene = SceneManager.sceneCount;
        LogHelper.GetInstance().Log(profile.masterScene.Bolden().Colorize(Color.green) + " counts " + numOfScene + " at start", true);
        for (int i = 0; i < numOfScene; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != profile.masterScene)
            {
                LogHelper.GetInstance().Log(profile.masterScene.Bolden().Colorize(Color.green) + " unloading " + scene.name, true);
                UnloadScene(scene.name);
            }
        }
    }

    public void LoadScenesList(List<string> scenesToLoad)
    {
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            this.LoadSceneAdditively(scenesToLoad[i]);
        }
    }
    public void UnloadScenesList(List<string> scenesToUnload)
    {
        for (int i = 0; i < scenesToUnload.Count; i++)
        {
            this.UnloadScene(scenesToUnload[i]);
        }
    }

    void Update()
    {
        ((GameState)gameStateManager.GetCurrentState()).UpdateState();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PostOffice.GetInstance().SendData(null, GameConsoleEvent.CONSOLE_SWITCH);
        }
    }

    public void PauseGame()
    {
        ChangeGameState(GameState.GameStateEnum.GamePaused);
    }
    public void UnPauseGame()
    {
        ChangeGameState(GameState.GameStateEnum.InGame);
    }
    public void GoToMainMenu()
    {
        ChangeGameState(GameState.GameStateEnum.MainMenu);
    }

    public void LoadSceneAdditively(string sceneName)
    {
        LogHelper.GetInstance().Log(" Loading Additively " + sceneName.Bolden() + "", true);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        if (SceneManager.GetActiveScene().name.Equals(profile.masterScene) == false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(profile.masterScene));
        }

    }
    public void LoadLevel(string levelName)
    {
        UnloadScenesList(profile.prequisiteGameScenes);
        LoadScenesList(profile.prequisiteGameScenes);
        LoadSceneAdditively(levelName);
        ChangeGameState(GameState.GameStateEnum.InGame);
    }

    private void UnloadCurrentLevel()
    {
        int numOfScene = SceneManager.sceneCount;
        for (int i = 0; i < numOfScene; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
            {
                UnloadScene(scene.name);
            }
        }
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }


    public void SetMouseVisibility(bool visibility)
    {
        Cursor.visible = visibility;
        Cursor.lockState = visibility ? CursorLockMode.None : CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        ChangeGameState(GameState.GameStateEnum.InGame);
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


    public StateManager GetStateManager()
    {
        return gameStateManager;
    }


    public bool IsInState(GameState.GameStateEnum stateToCheck)
    {
        if (gameStateManager == null)
        {
            return false;
        }
        else
        {
            return gameStateManager.GetCurrentState().GetEnum().Equals(stateToCheck);
        }
    }

    public void SetGameTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
    }
    private void ChangeGameState(GameState.GameStateEnum newState)
    {
        if (this.gameStateManager.RequestState(newState))
        {
            this.NotifyOnGameStateChange(newState);
        }
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
