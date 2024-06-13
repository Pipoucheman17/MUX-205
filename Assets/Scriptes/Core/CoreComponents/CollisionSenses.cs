using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform Fov { get => fov; private set => fov = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform fov;
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Vector2 fovDistance;


    public bool isDashing;
    public bool Grounded
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool TouchingWall
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool TouchingWallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool SeeSomething(LayerMask target)
    {
        return Physics2D.OverlapBox(Fov.position, fovDistance, 0f, target);
    }

    public Vector2 TargetSomething(LayerMask target)
    {
        Collider2D[] list = Physics2D.OverlapBoxAll(Fov.position, fovDistance, 0f, target);
        return list[0].transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        UnityEditor.Handles.DrawWireCube(Fov.position, fovDistance);

    }
}

