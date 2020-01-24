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
        var master = GameMaster.FindInstance();
        LogHelper.GetInstance().Log(this + " loading level with level index " + levelIndex);
        master.SetCurrentInGameLevel(levelIndex);
        master.GetStateManager().RequestState(GameState.GameStateEnum.InGame);
    }
}
