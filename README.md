# Projet M

Jeu de plateforme 2D inspiré de la serie de jeux Megaman Zero (GBA)

Présentation du projet :  ce projet de jeu à pour but de recréer le moteur de jeu de Megaman Zero avec Unity. Une partie de ce projet est inspiré de vidéo trouver sur internet pour le fonctionnement de base des machines à état présente dans le jeux avec des améliorations et des modifications.

Ce jeux se base sur l'utilisation de machines à état qui supprime l'utilisation des machines d'animation standard de Unity. Concrètement on retire au composant Animator l'arborescence de lien entre les états, qui ne foncionne plus qu'avec des booléen pour l'activation des animations. 

Pour la gestion des Inputs, il faut installer l'Inputs System Package de Unity.



## Fonctionnement des états

Les machines à état utiliser dans ce projet reposent toutes sur le même principe de fonctionnement. 

Une classe de gestion principal (Player ou Enemy) appelle les différents composant qui lui permette d'intéragir avec l'environnement (Mouvement, detections,armes...) et de décrire ses états. 

La machine à état est instantié et utiliser par le script porter par l'objet et sert à appeler les différents état que peux avoir le personnage. Les état determine la réaction à l'environnement mais aussi l'animation en cours. 

Les états appelle les différents composant présent dans le personnage pour lui faire adopter le bon comportement au bon état. Pour cela les états appelle le composant Core placer dans un objet enfant du personnage.

Le composant Core rassemble les fonctions de base (Deplacement, detections de collision etc...) et est utilisable aussi bien pour gérer le personnage jouable que les ennemies. 

 - Cf Image du schéma simplifié du fonctionnement du joueur. 
## State Machine

Cette partie sert à présenter le noyaux de fonctionnement du joueur.
Scripts de machine à état pour le joueur et pour les ennemies

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState CurrentState{get;private set;}

    public void Initialize(PlayerState startingState)
    { 
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    { 
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
```

Class State du player dont tous les états hérite

```
public class PlayerState
{

    protected Core core;

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected PlayerHealth playerHealth;
    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        isAnimationFinished = false;
        isExitingState = false;
        core = player.Core;
    }

    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;

    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck()
    {

    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
```


Class player qui sert à orchestrer et réunir tous les objets à faire fonctionner ensemble.

```
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
```
## Fonctions annexes

Ce projet comporte aussi des scripts concernant la gestion du jeu en général avec un menu principale et d'autre fonctionnalité (gestion de la musique, passage vers les crédits à la fin du niveau, arrière plan mouvant).

## Améliorations futures

Ce projet ne s'arrêtera pas la !

En effet je compte bien continuer à développer ce jeu pour parfaire ma maîtrise technique de Unity. 

Une petite liste des idées d'amélioration que j'ai en tête :

- Création de nouveaux niveaux
- Mise en place de nouvelles mécaniques liées au Level Design (ex: ascenseur)
- Ajout de nouveau type d'ennemies
- Ajout d'un boss
- Ajout de nouvelles armes disponibles pour le joueur
- Amélioration de l'UI 
- Amélioration graphique du Tiling des niveaux 
- Amélioration du comportement des ennemies 
- Amélioration du sound Design
