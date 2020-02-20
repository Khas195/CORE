using System;
using NaughtyAttributes;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int health;

    public int stamina;
}
