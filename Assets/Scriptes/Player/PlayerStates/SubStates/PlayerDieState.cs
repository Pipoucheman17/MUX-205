using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerGroundedState
{
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0f);
        core.Movement.SetVelocityY(0f);
        
    }

    public override void Exit()
    {
        base.Exit();
        //
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
