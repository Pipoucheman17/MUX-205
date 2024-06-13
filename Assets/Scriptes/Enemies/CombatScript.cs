using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour, IDamageable
{
    private Animator anim;
    [SerializeField] private float life;
    [SerializeField] private GameObject hitParticles;
    public void Damage(float amount)
    {
        Debug.Log("Damage : " + amount);
        anim.SetBool("damage",true);
        life = life - amount;
       Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
       
    }


    public void KillEnnemy()
    {
        if(life<= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void EndAnim()
    {
        anim.SetBool("damage",false);
        KillEnnemy();
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
}
