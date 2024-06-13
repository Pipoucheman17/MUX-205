using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityState : EnemyState
{

    protected bool isAbilityDone;

    private bool isGrounded;
    public EnemyAbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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
        isGrounded = core.CollisionSenses.Grounded;
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
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
