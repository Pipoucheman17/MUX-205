using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyGroundedState
{
    private int xInput;
    private Vector2 target;
    private float distanceToTarget;
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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
        if (!enemy.playerDetected)
        {
            enemy.targetPos = enemy.randomTarget();
            target = enemy.targetPos;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Actualise la position de la target
        if(enemy.playerDetected)
        {
            target = enemy.targetPos;
        }
        //Calcul la direction dans laquelle se diriger
        xInput = enemy.goToTarget(target);
        
        core.Movement.CheckIfShouldFlip(xInput);
        distanceToTarget = target.x - core.transform.position.x;
        core.Movement.SetVelocityX(enemyData.movementVelocity * xInput);
        if (Mathf.Abs(distanceToTarget) <= 2 || core.CollisionSenses.TouchingWall)
        {
            Debug.Log("BUllets :" + enemy.bulletLoad);
            if (enemy.playerDetected && enemy.bulletLoad>0)
            {
                stateMachine.ChangeState(enemy.E_AimState);
            }
            else
            {
                if (core.CollisionSenses.TouchingWall)
                {
                    core.Movement.CheckIfShouldFlip(-xInput);
                }
                stateMachine.ChangeState(enemy.E_IdleState);
            }
            
            
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
