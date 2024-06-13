using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyGroundedState
{
    private float randWaitTime;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        randWaitTime = Random.Range(3, 5);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(0f);
        core.Movement.SetVelocityY(0f);
        if (enemy.playerDetected && enemy.bulletLoad>0)
        {
            stateMachine.ChangeState(enemy.E_MoveState);
        }
        if (!enemy.playerDetected && (Time.time - startTime >= randWaitTime) && !isExitingState)
        {
            stateMachine.ChangeState(enemy.E_MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
