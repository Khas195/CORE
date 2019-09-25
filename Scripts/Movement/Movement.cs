﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : IMovement
{
    [SerializeField]
    [Tooltip("The RigidBody of the moving object")]
    Rigidbody targetRigidBody = null;
    int moveForward = 0;
    int moveSide = 0;
    bool jumpSignal = false;
    // cache 
    Transform targetTransform = null;
    private float distToGround;

    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(targetRigidBody);
        targetTransform = targetRigidBody.transform;
        Debug.Assert(targetTransform);
        distToGround = this.targetRigidBody.gameObject.GetComponent<Collider>().bounds.extents.y;
    }

    /// <summary>
    /// This function move its host game object at a constant speed. It uses the walkSpeed when moving the host object.
    /// This function move using the rigid body and will be called in Fixed Update
    /// </summary>
    /// <param name="forward"> Move the host game object forward or backward according to its facing. Possible value: -1, 0, 1</param>
    /// <param name="side"> Move the host game object left and right according to its side. Possible value: -1, 0, 1 </param>
    public override void Move(float forward, float side)
    {
        Debug.Assert(-1 <= forward && forward <= 1);
        Debug.Assert(-1 <= side && side <= 1);
        moveForward = (int)forward;
        moveSide = (int)side;
    }
    /// <summary>
    /// This function will signal the target object to jump in the next FixedUpdate
    /// </summary>
    public override void SignalJump()
    {
        jumpSignal = true;
    }
    void Step(float speed, int forward, int side, bool isAirborne)
    {
        
        var forwardDirection = targetTransform.forward * forward;
        var sideDirection = targetTransform.right * side;

        var moveDirection = forwardDirection + sideDirection;
        if (!isAirborne)
        {
            var velocity = moveDirection * speed + Vector3.up * targetRigidBody.velocity.y;
            targetRigidBody.velocity = velocity;
        }
        else
        {
            var velocity = moveDirection * speed;
            targetRigidBody.AddForce(velocity * 1.5f, ForceMode.Acceleration);
            targetRigidBody.velocity = Vector3.ClampMagnitude(targetRigidBody.velocity, speed);
        }
    }

    private void FixedUpdate()
    {
        bool isAirborned = !IsTouchingGround();
        float moveSpeed = 0;

        if (!isAirborned && moveForward < 0 && moveMode == MovementMode.Run)
        {
            moveSpeed = GetSpeedBasedOnMode(MovementMode.Walk);
        }
        else
        {
            moveSpeed = GetSpeedBasedOnMode(moveMode);
        }
        Step(moveSpeed, moveForward, moveSide, isAirborned);
        if (jumpSignal)
        {
            if (!isAirborned)
            {
                Jump();
            }
            jumpSignal = false;
        }
    }

    

    private void Jump()
    {
        targetRigidBody.AddForce(Vector3.up * data.jumpForce, ForceMode.Impulse);
    }
    public bool IsTouchingGround()
    {
        return Physics.Raycast(targetTransform.position, -Vector3.up, distToGround + 0.3f);
    }

    
}
