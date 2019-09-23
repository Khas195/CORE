using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRootMotion : IMovement
{
    [SerializeField]
    Animator anim = null;
    [SerializeField]
    float speedStep = 0;
    float targetForwardSpeed = 0;
    float targetSideSpeed = 0;
    public float lastForward { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var forwardSpeed = anim.GetFloat("Forward");
        forwardSpeed = Mathf.Lerp(forwardSpeed, targetForwardSpeed, speedStep);
        anim.SetFloat("Forward", forwardSpeed);

        var sideSpeed= anim.GetFloat("Side");
        sideSpeed = Mathf.Lerp(sideSpeed, targetSideSpeed, speedStep);
        anim.SetFloat("Side", sideSpeed);

    }

    public override void Move(float forward, float side)
    {
        Definition.MovementDebug("Forward Value: " + forward);
        targetForwardSpeed = forward == 0 ? 0 : this.GetSpeedBasedOnMode(moveMode);
        if (forward < 0)
        {
            targetForwardSpeed *= -1;
            targetForwardSpeed = Mathf.Clamp(targetForwardSpeed, -0.5f, 0);
        }
        Definition.MovementDebug("Side Value: " + forward);
        targetSideSpeed = side == 0 ? 0 : this.GetSpeedBasedOnMode(moveMode);
        if (side < 0)
        {
            targetSideSpeed *= -1;
        }

    }
    public override void SignalJump()
    {
        anim.SetTrigger("Jump");
    }
}
