using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 4f;

    [Header("Jump State")]
    public float jumpVelocity = 12f;
    public int amountOfJump = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Dash State")]
    public float dashVelocity = 8f;
    public float dashDuration = 1f;
    public float distanceBetweenImages = 1f;
    [Header("Attack State")]
    public float attackDelay = 1f;
    [Header("Hit State")]
    public float hitVelocity = 0.5f;
    public float hitTime = 1f;
}
