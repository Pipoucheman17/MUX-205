using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public int FacingDirection;
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    


    public bool isDashing { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        RB = GetComponentIn Parent<Rigidbody2D>();
        FacingDirection = 1;
    }
    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetIsDashing(bool dash)
    {
        isDashing = dash;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != core.Movement.FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        core.Movement.FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }


}
