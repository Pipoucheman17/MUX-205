using System.Collections;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{

    public static PlayerCombatManager instance;
    // Start is called before the first frame update

    public LayerMask ennemyLayer;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public Transform attackPoint3;
    public bool canSwordAttack;
    public bool swordAttack;

    private Transform attackPoint;
    public Vector2 attackRange;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SwordAttack();
    }

    public void SwordAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q) && PlayerMovement.instance.isGrounding == true)
        {
            if (canSwordAttack)
            {
                swordAttack = true;
                canSwordAttack = false;
            }
            else
            {
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && PlayerMovement.instance.isGrounding == false)
        {
            if (canSwordAttack)
            {
                canSwordAttack = false;
                PlayerMovement.instance.animator.SetTrigger("JumpSwordAttack");
                
                SwordAttackRange(1);
            }
            else
            {
                return;
            }
        }
    }


    public void InputManager()
    {
        if (!canSwordAttack)
        {
            canSwordAttack = true;
        }
        else
        {
            canSwordAttack = false;
        }
    }

    public void SwordAttackRange(int numAttack)
    {

        if (numAttack == 1)
        {
            attackRange = new Vector2(2f, 0.92f);
            attackPoint = attackPoint1;
        }
        if (numAttack == 2)
        {
            attackRange = new Vector2(2f, 1.3f);
            attackPoint = attackPoint2;
        }
        if (numAttack == 3)
        {
            attackRange = new Vector2(2f, 1.75f);
            attackPoint = attackPoint3;
        }
        Collider2D[] hitEnnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, ennemyLayer);

        foreach (Collider2D hit in hitEnnemies)
        {
            Debug.Log("We hit" + hit.name);
            hit.GetComponent<EnemiesHealth>().TakeDamage(1);
        }
    }

}
