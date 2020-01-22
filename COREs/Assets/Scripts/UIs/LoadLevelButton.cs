using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadLevelButton : MonoBehaviour
{
    [SerializeField]
    int levelIndex = 0;
    [SerializeField]
    Button button = null;

    void Awake()
    {
        FindButton();
    }
    [Button("Assign Button")]
    public void FindButton()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        var master = GameMaster.GetInstance();
        if (master == null)
        {
            LogHelper.GetInstance(forceAddInEditor: true).LogWarning(this + " need an instance of GameMaster in the scene to work");
        }
        master.SetCurrentInGameLevel(levelIndex);
        master.GetStateManager().RequestState(GameState.GameStateEnum.InGame);
    }
}
