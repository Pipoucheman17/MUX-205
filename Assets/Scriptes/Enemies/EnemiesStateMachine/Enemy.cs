using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region State Variables
    public EnemyStateMachine E_StateMachine { get; private set; }
    public EnemyIdleState E_IdleState { get; private set; }
    public EnemyMoveState E_MoveState { get; private set; }
    public EnemyAttackState E_AttackState { get; private set; }
    public EnemyAimState E_AimState { get; private set; }
    public EnemyHitState E_HitState { get; private set; }
    public EnemyDieState E_DieState { get; private set; }

    [SerializeField]private EnemyData enemyData;
    public GameObject Bullet;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public EnemyHealth Health { get; private set; }
    public Core Core { get; private set; }
    public AudioSource Audio { get; private set; }
    #endregion

    #region Others Variables
    [SerializeField] public LayerMask targetMask;

    public Vector2 targetPos;
    public bool playerDetected;
    private float enterloadTime;
    public float bulletLoad { get; private set; }
    public bool isReloading { get; private set; }
    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        E_StateMachine = new EnemyStateMachine();
        E_IdleState = new EnemyIdleState(this, E_StateMachine, enemyData, "idle");
        E_MoveState = new EnemyMoveState(this, E_StateMachine, enemyData, "move");
        E_AttackState = new EnemyAttackState(this, E_StateMachine, enemyData, "attack");
        E_AimState = new EnemyAimState(this, E_StateMachine, enemyData, "aim");
        E_HitState = new EnemyHitState(this, E_StateMachine, enemyData, "hit");
        E_DieState = new EnemyDieState(this, E_StateMachine, enemyData, "die");
    }
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        Health = GetComponent<EnemyHealth>();
        Audio = GetComponent<AudioSource>();
        E_StateMachine.Initialize(E_IdleState);
        enterloadTime = 0f;
        bulletLoad = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Core.LogicUpdate();
        E_StateMachine.CurrentState.LogicUpdate();
        if (bulletLoad == 0 && !isReloading)
        {
            enterloadTime = Time.time;
            isReloading = true;
        }
        if (Time.time - enterloadTime > enemyData.loadingDelay && isReloading)
        {
            reloadWeapon();
            isReloading = false;
        }
    }
    private void FixedUpdate()
    {
        E_StateMachine.CurrentState.PhysicsUpdate();
    }


    private void AnimationTrigger() => E_StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => E_StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion

    #region Other Functions
    public int goToTarget(Vector2 targetPos)
    {
        Vector2 move = targetPos - (Vector2)Core.transform.position;
        move.y = 0;
        if (move.x > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public Vector2 randomTarget()
    {
        Vector2 randomTarget = Random.onUnitSphere;
        randomTarget = randomTarget.normalized * Random.Range(-2, 2);
        randomTarget.y = transform.position.y;
        return randomTarget;
    }

    public void reloadWeapon()
    {
        bulletLoad = 3;
    }

    public void Fire()
    {
        Debug.Log(bulletLoad);
        bulletLoad -= 1;
        
        GameObject cloneBullet = Instantiate(Bullet,Core.Attack.Canon.position,Core.Attack.Canon.rotation);
        cloneBullet.GetComponent<bulletShot>().setDirection(Core.Movement.FacingDirection);
    }

    public void TakeDamage()
    {
        if(Health.health <=0)
        {
            E_StateMachine.ChangeState(E_DieState);
        }
        else
        {
            E_StateMachine.ChangeState(E_HitState);
        }
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    #endregion
}
