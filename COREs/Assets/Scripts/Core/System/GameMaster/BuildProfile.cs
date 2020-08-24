using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Build Profile", menuName = "Build Settings/Build Profile", order = 1)]
public class BuildProfile : ScriptableObject
{
    [Scene]
    public string masterScene = "";
    [SerializeField]
    public bool skipMainMenu = false;
    [Scene]
    public List<string> setupScenes = new List<string>();
    [Scene]
    public List<string> menuScenes = new List<string>();
    [Scene]
    public List<string> prequisiteGameScenes = new List<string>();
    [Scene]
    public List<string> levels = new List<string>();
}
