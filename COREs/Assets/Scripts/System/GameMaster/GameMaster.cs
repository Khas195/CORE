using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : SingletonMonobehavior<GameMaster>
{
    [SerializeField]
    [Required]
    StateManager gameStateManager = null;
    [SerializeField]
    int startLevelIndex = 0;
    [SerializeField]
    int currentInGameLevelIndex = 0;
    public void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
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

    public void SetCurrentInGameLevel(int levelIndex)
    {
        currentInGameLevelIndex = levelIndex;
    }
    public StateManager GetStateManager()
    {
        return gameStateManager;
    }

    public int GetStartLevelIndex()
    {
        return startLevelIndex;
    }
    public int GetInGameLevelIndex()
    {
        return currentInGameLevelIndex;
    }
}
