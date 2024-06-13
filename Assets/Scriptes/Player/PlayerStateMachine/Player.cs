using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState ClimbState { get; private set; }
    public PlayerWallGrabState GrabState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; protected set; }
    public PlayerDashState DashState { get; protected set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    public PlayerDieState DieState { get; private set; }

    public PlayerHitState HitState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerHealth Health { get; private set; }
    public Core Core { get; private set; }

    public AudioSource Audio {get;private set;}
    #endregion

    #region Other Variables
    public Transform CurrentPosition { get; private set; }
  //  public Vector2 CurrentVelocity { get; private set; }
  //  public int core.Movement.FacingDirection { get; private set; }
    private Vector2 workspace;
    public bool isHit { get; private set; }
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "wallJump");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        DieState = new PlayerDieState(this, StateMachine, playerData, "die");
        HitState = new PlayerHitState(this, StateMachine, playerData, "hit");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        Inventory = GetComponent<PlayerInventory>();
        Health = GetComponent<PlayerHealth>();
        Audio = GetComponent<AudioSource>();
        StateMachine.Initialize(IdleState);
        
        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //  SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    

    public void SetIsHit(bool hit)
    {
        isHit = hit;
    }

    #endregion




    #region Other Functions
    public bool CheckIfInvicible()
    {
        return Health.isInvicible;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
   

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);
        if (Health.currentHealth <= 0)
        {
            StateMachine.ChangeState(DieState);
        }
        else
        {
            StateMachine.ChangeState(HitState);
        }
    }




    #endregion
}
