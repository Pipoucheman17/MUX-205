using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    private bool jumpInput;
    private bool jumpInputStop;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;

    private float startWallJumpCoyoteTime;
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        core.Movement.SetVelocityY(-playerData.wallSlideVelocity);
        core.Movement.SetVelocityX(0f);
        core.Movement.SetIsDashing(false);


        if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            isTouchingWall = core.CollisionSenses.TouchingWall;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
    }

    public override void DoCheck()
    {
        base.DoCheck();
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = core.CollisionSenses.Grounded;
        isTouchingWall = core.CollisionSenses.TouchingWall;
        isTouchingWallBack = core.CollisionSenses.TouchingWallBack;
      
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }


}
