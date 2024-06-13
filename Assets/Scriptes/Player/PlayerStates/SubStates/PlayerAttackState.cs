using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{

    private float velocityToSet;
    private bool setVelocity;
    private Weapon weapon;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.EnterWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(setVelocity)
        {
            core.Movement.SetVelocityX(velocityToSet*core.Movement.FacingDirection);
        }
    }

    public override void Exit()
    {
        weapon.ExitWeapon();
        base.Exit();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }
    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    #endregion
    public void SetPlayerVelocity(float velocity)
    {
        core.Movement.SetVelocityX(velocity*core.Movement.FacingDirection);
        velocityToSet = velocity;
        setVelocity= true;
    }
}

