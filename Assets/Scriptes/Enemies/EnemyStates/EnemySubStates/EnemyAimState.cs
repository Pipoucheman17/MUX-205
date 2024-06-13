using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimState : EnemyAbilityState
{
    public EnemyAimState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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
        core.Movement.SetVelocityX(0f);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(0);
        core.Movement.CheckIfShouldFlip(enemy.goToTarget(enemy.targetPos));
        if (isAnimationFinished && enemy.bulletLoad > 0)
        {
            stateMachine.ChangeState(enemy.E_AttackState);
        }
        else if(!enemy.playerDetected)
        {
            stateMachine.ChangeState(enemy.E_MoveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
