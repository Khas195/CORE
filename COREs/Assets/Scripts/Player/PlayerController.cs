using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnPlayerSeeObject : UnityEvent<GameObject>
{

}
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    IMovement moveBehavior = null;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible =false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    // Update is called once per frame
    void Update()
    {
        ControlMovement();
    }
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
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveBehavior.SetMovementMode(IMovement.MovementMode.Walk);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveBehavior.SignalJump();
        }
        moveBehavior.Move((int)vertical, (int)horizontal);
    }
}
