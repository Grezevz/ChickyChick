using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 60;

    [Header("Jump State")]
    public float jumpVelocity = 15;

    [Header("Check Variables")]
    public float rotation = 3.0f;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
