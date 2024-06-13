using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Core core;

    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;
    protected EnemyHealth enemyHealth;
    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
        isAnimationFinished = false;
        isExitingState = false;
        core = enemy.Core;
    }

    public virtual void Enter()
    {
        DoCheck();
        enemy.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;

    }
    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        if (core.CollisionSenses.SeeSomething(enemy.targetMask))
        {
            enemy.playerDetected = true;
            enemy.targetPos = core.CollisionSenses.TargetSomething(enemy.targetMask);
        }
        else
        {
            enemy.playerDetected = false;
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck()
    {
      
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
