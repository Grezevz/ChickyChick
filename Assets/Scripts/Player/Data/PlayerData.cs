using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 60;

    [Header("Jump State")]
    public float jumpVelocity = 15;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]
    public float rotation = 3.0f;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
