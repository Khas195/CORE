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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveBehavior.SetMovementMode(IMovement.MovementMode.Run);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveBehavior.SetMovementMode(IMovement.MovementMode.Walk);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveBehavior.SignalJump();
        }
        moveBehavior.Move(vertical, horizontal);
    }
}
