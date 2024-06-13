using UnityEngine;
using System.Collections;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public Player player;
    public int maxHealth = 10;

    public int currentHealth;
    public SpriteRenderer graphics;
    public float invicibilityTime = 2f;
    public float invicibilityFlashDelay = 0.2f;
    public bool isInvicible = false;
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(5);
        }
    }

    public void Damage(float _damage)
    {
        TakeDamage((int)_damage);
    }
    public void TakeDamage(int _damage)
    {
        if (!isInvicible)
        {

            currentHealth -= _damage;
            healthBar.SetHealth(currentHealth);
            if (currentHealth > 0)
            {
                isInvicible = true;
                StartCoroutine(InvicibilityFlash());
                StartCoroutine(HandleInvicibilityDelay());
            }
        }



    }
    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }


    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTime);
        isInvicible = false;
    }
}
