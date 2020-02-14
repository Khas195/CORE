using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.CustomAttributes;
using NaughtyAttributes;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    Rigidbody2D body = null;
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    IMovement movement = null;


    // Start is called before the first frame update
    void Start()
    {
        movement.SetRigidBody2D(body);
    }

    public void Jump()
    {
        movement.SignalJump();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(int horizontal, int vertical)
    {
        movement.Move(vertical, horizontal);
    }
}
