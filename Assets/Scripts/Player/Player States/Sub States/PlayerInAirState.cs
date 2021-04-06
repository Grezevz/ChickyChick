public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;

    public PlayerInAirState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName) {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (isGrounded && player.CurrentVelocity.y < 0.01f) { stateMachine.ChangeState(player.LandState); return; }

        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(playerData.movementVelocity * xInput);

        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
