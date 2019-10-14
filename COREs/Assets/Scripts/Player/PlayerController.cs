using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/**
 * This class processes the player's input and call the coresspondence behavior to perform the intended action
 */
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    /** \brief Reference to the movement interface */
    IMovement moveBehavior = null;
    [SerializeField]
    CharacterAnimatorControl animControl;

    void Update()
    {
        ControlMovement();
    }
    /**
     * All inputs by the player are and should be done in this function .
     * Current Handled Input: . 
     * LeftShift (switching between run and walk mode) .
     * Space (Calls SignalJump function in IMovement) .
     * Recieved Horizontal and vertical then call the Move function in IMovement with those parameters .
     */
    private void ControlMovement()
    {

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButtonDown(0) && moveBehavior.IsTouchingGround() == true)
        {
            animControl.TriggerAttackAnimation();
            Debug.Log("Character Attack");
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveBehavior.SetMovementMode(IMovement.MovementMode.Run);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveBehavior.SetMovementMode(IMovement.MovementMode.Walk);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && moveBehavior.IsTouchingGround() == true && animControl.IsAttacking() == false)
        {
            moveBehavior.SignalJump();
        }
        if (animControl.IsAttacking() == true)
        {
            horizontal = 0;
            vertical = 0;
        }
        moveBehavior.Move(vertical, horizontal);

    }
}
