using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 60;


    [Header("Grab State")]
    public float grabMovementVelocity = 30;
    public float grabDistance = 10f;
    public float grabDistanceRelease = 20f;


    [Header("Jump State")]
    public float jumpVelocity = 15;
    public int amountOfJumps = 1;


    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;


    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImage = 0.5f;


    [Header("Check Variables")]
    public float rotation = 3.0f;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
