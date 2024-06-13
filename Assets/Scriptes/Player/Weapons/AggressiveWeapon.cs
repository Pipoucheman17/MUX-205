using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    public AudioSource audio;
    protected override void Awake()
    {
        base.Awake();
        audio = GetComponent<AudioSource>();
        
        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong Data for the Weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        audio.Play();
        CheckMeleeAttack();
        
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];
        foreach (IDamageable item in detectedDamageables.ToList())
        {
            Debug.Log(details.damageAmount);
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
    
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
           
            detectedDamageables.Add(damageable);
        }

    }

    public void RemoveFromDetected(Collider2D collision)
    {
     
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
        
            detectedDamageables.Remove(damageable);
        }
    }


}
