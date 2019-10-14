using UnityEngine;
/**
 * The IMovement interface acts as a generalization of all type of movements in the game.!--
 * It is used by the Player controller to move the target gameobject
 */
public abstract class IMovement : MonoBehaviour {

    /**
     * Different types of movement mode in the game
     */
    public enum MovementMode
    {
        Walk, 
        Run 
    }
    [SerializeField]
    /** The container for all movement related data */
    protected MovementData data = null;
    /** The current movement mode */
    protected MovementMode moveMode = MovementMode.Walk;
    /** All movements actions should be handle in this function*/
    public abstract void Move (float forward, float side);
    /** Signaled that the jump command had been called */
    public abstract void SignalJump();
    /** Set the current movement mode */
    public void SetMovementMode(MovementMode newMode)
    {
        moveMode = newMode;
    }
    /** 
     * Get the correspondence speed in the data container(MovementData) based on the currnt movement mode
     */
    protected float GetSpeedBasedOnMode()
    {
        float moveSpeed;
        switch (moveMode)
        {
            case MovementMode.Run:
                moveSpeed = data.runSpeed ;
                break;
            case MovementMode.Walk:
                moveSpeed = data.walkSpeed;
                break;
            default:
                moveSpeed = data.runSpeed;
                break;
        }

        return moveSpeed;
    }
    public virtual bool IsTouchingGround() {
        return true;
    }
}
