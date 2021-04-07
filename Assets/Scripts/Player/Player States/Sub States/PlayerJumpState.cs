public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName) {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }
    
    public override void Enter() {
        base.Enter();

        player.JumpSquash();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump() {
        if (amountOfJumpsLeft > 0) { return true; }
        return false;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}