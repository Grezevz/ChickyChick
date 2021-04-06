public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        JumpInput = player.InputHandler.JumpInput;

        if (JumpInput) { 
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState); 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
