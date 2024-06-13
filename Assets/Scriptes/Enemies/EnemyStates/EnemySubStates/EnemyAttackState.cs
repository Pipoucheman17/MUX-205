using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAbilityState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        enemy.Fire();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0);
        enemy.Audio.PlayOneShot(enemyData.playList[1]);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(0);
        if (isAnimationFinished)
        {
            if (enemy.bulletLoad > 0)
            {
                stateMachine.ChangeState(enemy.E_AttackState);
            }
                if (enemy.bulletLoad <= 0)
                {
                    stateMachine.ChangeState(enemy.E_IdleState);
                }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
