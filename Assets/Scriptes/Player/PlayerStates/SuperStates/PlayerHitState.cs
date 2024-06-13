using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    protected bool isPlayerHit;

    private int xInput;
    public PlayerHitState(Player player,PlayerStateMachine stateMachine,PlayerData playerData, string animBoolName) : base (player,stateMachine,playerData,animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        xInput = player.InputHandler.NormInputX;
        core.Movement.SetVelocityX(playerData.hitVelocity * xInput);
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (startTime + playerData.hitTime< Time.time)
        {
            player.SetIsHit(false);
            player.StateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
