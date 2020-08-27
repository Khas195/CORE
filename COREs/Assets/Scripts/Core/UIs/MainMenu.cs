using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        GameMaster.GetInstance().ExitGame();
    }
    public void LoadFistLevel()
    {
    }
}
