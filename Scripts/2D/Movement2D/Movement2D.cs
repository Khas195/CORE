using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : IMovement
{
    [SerializeField]
    Rigidbody2D body2D = null;
    float currentSpeed;
    bool inMotion = false;
    [SerializeField]
    float smoothAcceleration = 0;

    float cachedSide;

    // Start is called before the first frame update
    void Start()
    {
        if (this.data == null)
        {
            Definition.InitalizeErrors("Move Data is null in " + this);
        }

    }
    void Update()
    {
        if (inMotion == true && currentSpeed < data.runSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, data.runSpeed, smoothAcceleration);
        }
    }
    public override void Move(float forward, float side)
    {
        if (forward != 0 || side != 0)
        {
            inMotion = true;
        }
        else
        {
            inMotion = false;
            currentSpeed = 0;
        }
        var vel = new Vector3();
        vel.y = forward * currentSpeed * Time.deltaTime;
        vel.x = side * currentSpeed * Time.deltaTime;
        body2D.MovePosition(body2D.transform.position + vel);
        cachedSide = side;
    }
    public bool IsInMotion() {
        return inMotion;
    }
    public float GetSideInput(){
        return cachedSide;
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    public override void SignalJump()
    {
        return;
    }
}
