using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float _Damage)
    {
        Debug.Log("DAMAGE");
        health -= _Damage;
        enemy.TakeDamage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
