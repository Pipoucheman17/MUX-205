using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private bool isGrounded;
    private int xInput;
    private float lastImageXpos;
    private bool jumpInput;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = core.CollisionSenses.Grounded;
    }
    public bool CanDash()
    {
        if (core.CollisionSenses.Grounded)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0f);
        core.Movement.SetIsDashing(true);
        player.Audio.PlayOneShot(player.Audio.clip);
    }

    public override void LogicUpdate()
    {
        isGrounded = core.CollisionSenses.Grounded;
        core.Movement.SetIsDashing(true);
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        core.Movement.CheckIfShouldFlip(xInput);
        core.Movement.SetVelocityX(playerData.dashVelocity * xInput);
        if (Mathf.Abs(core.transform.position.x - lastImageXpos) > playerData.distanceBetweenImages)
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = core.transform.position.x;
        }
        if ((Time.time >= startTime + playerData.dashDuration && isGrounded) || player.InputHandler.CheckIfShouldStopDash())
        {
            isAbilityDone = true;
            core.Movement.SetIsDashing(false);
            stateMachine.ChangeState(player.IdleState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            core.Movement.SetIsDashing(true);
            stateMachine.ChangeState(player.JumpState);
        }

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        
    }
}
