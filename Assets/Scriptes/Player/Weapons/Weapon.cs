using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;
    protected float resetTime;
    protected int attackCounter;
    protected Animator weaponAnimator;
    protected PlayerAttackState state;
    protected Player player;
    protected virtual void Awake()
    {
        weaponAnimator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }
        weaponAnimator.SetBool("attack", true);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
        
    }


    public virtual void ExitWeapon()
    {
        weaponAnimator.SetBool("attack", false);
        attackCounter++;
        gameObject.SetActive(false);
    }

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }



    public virtual void AnimationActionTrigger()
    {
        
    }
    #endregion

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }


}

