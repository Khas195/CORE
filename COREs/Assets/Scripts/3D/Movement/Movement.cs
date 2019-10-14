using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class handles all movement related behaviors in 3D
 * The host object (mentioned in blow) is the object which this script will perform its functions on .!--
 * The host object is not necessary be the parent gameobject of the script.!-- 
 */
public class Movement : IMovement
{
    [SerializeField]
    [Tooltip("The RigidBody of the moving object")]
    /** The rigid body of the host object.!--
     * Needed to be assigned in the unity editor
     */
    Rigidbody targetRigidBody = null;
    /**
     * Decide if the host object should move foward accodring the camera's facing direction.!--
     * Instead of move forward according to its own facing direction
     */
    [SerializeField]
    bool shouldMoveTowardCameraDirection;
    [SerializeField]
    /**
     * The rotate speed of the host object toward the camera's facing direction.!--
     * Not needed if shouldMoveTowardCameraDirection is false 
     */
    float rotateSpeed;
    [SerializeField]
    /**
     * The camera entity that the host entity is facing toward.!--
     * Needed to be assigned in the unity editor if shouldMoveTowardCameraDirection is true
     */
    GameObject cameraYawPivot;
    /**
     * The list of points which is needed to know whether the host object is airborned or not
     */
    [SerializeField]
    List<Transform> checkGroundsList;
    /** cached the forward value in the Move function*/
    int moveForward = 0;
    /** cached the side value in the Move function*/
    int moveSide = 0;
    /** cached the jump signal in the Signal Jump function*/
    bool jumpSignal = false;
    /** Cached transform of the host object */
    Transform targetTransform = null;
    void Start()
    {
        Initalize();
    }
    /** 
     * Initalize all the cached variables 
     */
    private void Initalize()
    {
        targetTransform = targetRigidBody.transform;
    }

    /**
     * This function received the player's inputs (forward and side) then saved them to be processed in the next fixed update.
     * If shouldMoveTowardCameraDirection is true then it will rotate the host game object toward the camera's faciing direction first.
     * \param fordward is how much the host game object should move forward and backward
     * \param side is how much the host game object should move sideway
     */
    public override void Move(float forward, float side)
    {
        if (shouldMoveTowardCameraDirection)
        {
            if (forward != 0 || side != 0)
            {
                // Choose whether the forward has a bigger value or the side
                moveForward = (int)(Mathf.Abs(forward) > Mathf.Abs(side) ? forward : side);

                moveForward = Mathf.Abs(moveForward);

                RotateTowardCameraDirection(forward, side);
            }
            else
            {
                moveForward = 0;
                moveSide = 0;
            }
        }
        else
        {
            moveForward = (int)forward;
            moveSide = (int)side;
        }
        Definition.MovementDebug("Movement(forward, side): " + moveForward + ", " + moveSide);
    }

    /**
     * Rotate the host game object toward the camera entity's facing direction
     * \param fordward is how much the host game object should move forward and backward
     * \param side is how much the host game object should move sideway
     */
    private void RotateTowardCameraDirection(float forward, float side)
    {
        var forwardDir = cameraYawPivot.transform.forward * forward;
        var sideDir = cameraYawPivot.transform.right * side;
        var moveDir = forwardDir + sideDir;
        moveDir.y = 0;
        Definition.MovementDebug("Camera Move Direction" + moveDir);
        var rotation = Quaternion.LookRotation(moveDir);
        targetRigidBody.rotation = Quaternion.Slerp(targetRigidBody.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    /**
    * This function will signal the target object to jump in the next FixedUpdate.
    * If the character's jump is on cd then nothing will happen.
    */
    public override void SignalJump()
    {
        jumpSignal = true;
    }
    /**
     * This function move the host object according to the forward and side inputs of the player.
     * The movement is performed by manipulating the velocity property of the rigidbody.
     * The speed of the movement is reduced if the host object is airborned.
     * \param speed is the desired movement speed for the host object.
     * \param forward is how much the player need to move forward or backward. 
     * \param side is how much the player need to move sideway.
     */
    void Step(float speed, int forward, int side)
    {
        var forwardDirection = targetTransform.forward * forward;
        var sideDirection = targetTransform.right * side;

        var moveDirection = forwardDirection + sideDirection;
        Definition.MovementDebug("Movement Direction: " + moveDirection);
        var velocity = moveDirection * speed + Vector3.up * targetRigidBody.velocity.y;
        targetRigidBody.velocity = velocity;

        Definition.MovementDebug("Movement Velocity after each step: " + targetRigidBody.velocity);
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    /**
     * This function process the movement of the host object according to the forward, side and jump inputs
     * The movement speed is also changed if the host object is airborned
     */
    private void ProcessMovement()
    {
        if (jumpSignal)
        {
            Jump();
            jumpSignal = false;
        }
        float moveSpeed = 0;
        moveSpeed = GetSpeedBasedOnMode();
        Step(moveSpeed, moveForward, moveSide);
    }

    /** 
     * Perform the jumping action by adding an impulse force to the host's rigid body according to its up direction.!--
     */
    private void Jump()
    {
        Debug.Log("Character jump with force of " + data.jumpForce);
        targetRigidBody.AddForce(Vector3.up * data.jumpForce, ForceMode.Impulse);
    }
    /**
     * Check whether the rigid body of the host object is touching the ground by raycasting from the list of points (checkGroundsList)
     * It is important to know that the desired pivot should be on the bottom of the object.
     */
    public override bool IsTouchingGround()
    {
        foreach (var trans in checkGroundsList)
        {
            if (Physics.Raycast(trans.position, -Vector3.up, 0.1f))
            {
                return true;
            }
        }
        return false;
    }
    


}
