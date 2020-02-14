using System;
using Assets.Scripts.Core.CustomAttributes;
using NaughtyAttributes;
using UnityEngine;
/**
 * The IMovement interface acts as a generalization of all type of movements in the game.!--
 * It is used by the Player controller to move the target gameobject
 */
public class IMovement : MonoBehaviour
{

    /**
     * Different types of movement mode in the game
     */
    public enum MovementType
    {
        Walk,
        Run
    }
    [SerializeField]
    protected bool noCharacter;
    /** The container for all movement related data */
    [InfoBox("Movement Data Object - The values below are applied for script that use this Data Object", EInfoBoxType.Warning)]
    [Required]
    [SerializeField]
    [DisplayScriptableObjectProperties]
    [BoxGroup("Movement Data")]
    protected MovementData data = null;



    /** The current movement mode */
    protected MovementType moveMode = MovementType.Walk;
    /** All movements actions should be handle in this function*/
    public virtual void Move(float forward, float side) { return; }

    public virtual float GetCurrentSpeed()
    {
        return 0;
    }

    /** Signaled that the jump command had been called */
    public virtual void SignalJump()
    {
        return;
    }

    public MovementType GetCurrentMoveMode()
    {
        return moveMode;
    }

    public virtual bool HadMoveCommand()
    {
        return true;
    }

    /// <summary>
    /// Set the rigid body for the movement behavior.
    /// </summary>
    /// <param name="hostRigidBody"> The rigidbody of the character's model</param>
    public virtual void SetRigidBody(Rigidbody hostRigidBody)
    {
    }
    public virtual void SetRigidBody2D(Rigidbody2D body)
    {
        return;
    }

    /** Set the current movement mode */
    public void SetMovementMode(MovementType newMode)
    {
        moveMode = newMode;
    }
    /// <summary>
    /// Set the movement data.
    /// </summary>
    /// <param name="movementData">The movement data</param>
    public void SetMovementData(MovementData movementData)
    {
        this.data = movementData;
    }

    /** 
* Get the correspondence speed in the data container(MovementData) based on the currnt movement mode
*/
    protected float GetSpeedBasedOnMode()
    {
        float moveSpeed;
        switch (moveMode)
        {
            case MovementType.Run:
                moveSpeed = data.runSpeed;
                break;
            case MovementType.Walk:
                moveSpeed = data.walkSpeed;
                break;
            default:
                moveSpeed = data.runSpeed;
                break;
        }

        return moveSpeed;
    }
    public virtual bool IsTouchingGround()
    {
        return true;
    }
    public virtual MovementData GetMovementData()
    {
        return this.data;
    }

    public virtual void RotateToward(Vector3 direction, bool rotateY)
    {
    }
}
