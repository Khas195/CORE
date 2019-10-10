using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MovementData", menuName ="Data/Movement", order = 1)]
/**
 * The container for all movement related datas in the game
 * Can be created in the Unity Editor.!-- 
 * Right Click in Folder View -> Data -> movement
 */
public class MovementData : ScriptableObject
{
    public float walkSpeed = 10;
    public float runSpeed = 20;
    public float jumpForce = 50;
}
