using NaughtyAttributes;
using UnityEngine;

public class Movement2DPlatform : IMovement
{


    [SerializeField]
    [Required]
    [ShowIf("noCharacter")]
    Rigidbody2D body2D = null;
    [SerializeField]
    float maxSpeed = 10;
    [SerializeField]
    float fallMultiplier = 2.5f;
    float cachedSide = 0;
    private bool jumpSignal;


    public override void Move(float forward, float side)
    {
        cachedSide = side;
    }
    void Update()
    {

        ProcessMovement();
        if (jumpSignal)
        {
            if (this.IsTouchingGround())
            {
                var curVel = body2D.velocity;
                curVel.y = data.jumpForce;
                body2D.velocity = curVel;
            }
            jumpSignal = false;
        }
        if (body2D.velocity.y < 0)
        {
            body2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        if (body2D.velocity.magnitude > maxSpeed)
        {
            body2D.velocity = Vector3.ClampMagnitude(body2D.velocity, maxSpeed);
        }
    }

    void FixedUpdate()
    {

    }
    private void ProcessMovement()
    {
        var curVel = body2D.velocity;
        curVel.x = (cachedSide * data.runSpeed);
        body2D.velocity = curVel;
    }

    public override void SignalJump()
    {
        jumpSignal = true;
    }
    public override void SetRigidBody2D(Rigidbody2D body)
    {
        this.body2D = body;
    }

}
