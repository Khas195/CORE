using UnityEngine;

public abstract class IMovement : MonoBehaviour {

    public enum MovementMode
    {
        Walk, 
        Run 
    }
    [SerializeField]
    protected MovementData data = null;
    protected MovementMode moveMode = MovementMode.Walk;
    public abstract void Move (float forward, float side);
    public abstract void SignalJump();
    public void SetMovementMode(MovementMode newMode)
    {
        moveMode = newMode;
    }
    protected float GetSpeedBasedOnMode(MovementMode curMoveMode)
    {
        float moveSpeed;
        switch (curMoveMode)
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
}
