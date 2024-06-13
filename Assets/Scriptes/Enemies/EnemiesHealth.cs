using UnityEngine;

public class EnemiesHealth : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;

    public Collider2D hitBox;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }


  
    void Recoil()
    {

    }

    void EnnemyDeath()
    {
        
    }
}
