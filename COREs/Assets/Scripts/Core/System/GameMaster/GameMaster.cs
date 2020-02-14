﻿using System;
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
    int currentInGameLevelIndex = 1;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        String masterSceneName = "MasterScene";
        UnloadAllScenesExcept(masterSceneName);
    }

    private void UnloadAllScenesExcept(string sceneNotToUnloadName)
    {
        int numOfScene = SceneManager.sceneCount;
        for (int i = 0; i < numOfScene; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneNotToUnloadName)
            {
                UnloadScene(scene.name);
            }
        }
    }

    public void LoadSceneAdditively(string sceneName)
    {
        LogHelper.GetInstance().Log(" Loading <b>" + sceneName + "</b>", true);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public void LoadLevel(int levelIndex)
    {
        LogHelper.GetInstance().Log(" Loading <b> Level" + levelIndex + "</b>", true);
        SceneManager.LoadScene("Level" + levelIndex, LoadSceneMode.Single);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = currentInGameLevelIndex + 1;
        LogHelper.GetInstance().Log("Checking if level " + nextLevelIndex + " can be loaded", true);
        if (Application.CanStreamedLevelBeLoaded("Level" + nextLevelIndex))
        {
            LoadLevel(nextLevelIndex);
        }
        else
        {
            LogHelper.GetInstance().LogWarning("Level " + nextLevelIndex + " could not be loaded", true);
            LogHelper.GetInstance().Log("Restarting current level instead", true);
            RestartLevel();
        }
    }

    public void SetMouseVisibility(bool visibility)
    {
        Cursor.visible = visibility;
        Cursor.lockState = visibility ? CursorLockMode.None : CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        this.gameStateManager.RequestState(GameState.GameStateEnum.InGame);
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
    public void RestartLevel()
    {
        LogHelper.GetInstance().Log("<b>Restarting Level: " + currentInGameLevelIndex + " </b>", true);
        this.LoadLevel(currentInGameLevelIndex);
    }
}
