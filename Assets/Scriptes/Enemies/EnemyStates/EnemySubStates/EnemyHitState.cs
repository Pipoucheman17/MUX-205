using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(enemy.E_IdleState);
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(enemyData.hitVelocity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(enemyData.hitVelocity * core.Movement.FacingDirection);
        core.Movement.CheckIfShouldFlip(-enemy.goToTarget(enemy.targetPos));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
