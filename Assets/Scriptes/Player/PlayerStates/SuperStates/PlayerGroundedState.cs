using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;

    private bool JumpInput;
    private bool DashInput;
    private bool DieInput;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = core.CollisionSenses.Grounded;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0f);
        player.JumpState.ResetAmountOfJumpLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        DashInput = player.InputHandler.DashInput;
        DieInput = player.InputHandler.DieInput;

        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (JumpInput && player.JumpState.CanJump())
        {

            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (player.DashState.CanDash() && DashInput && Mathf.Abs(core.Movement.CurrentVelocity.x) > 0.1f)
        {
            player.InputHandler.UseDashInput();
            stateMachine.ChangeState(player.DashState);
        }
        else if (DieInput)
        {
            stateMachine.ChangeState(player.DieState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
