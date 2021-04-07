public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool GrabInput;
    protected bool GrabDistance;
    protected bool GrabDistanceRelease;
    private bool JumpInput;
    private bool isGrounded;
    
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
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
        GrabInput = player.InputHandler.GrabInput;

        if (JumpInput && player.JumpState.CanJump()) { 
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState); 
        } else if (!isGrounded) { 
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState); 
        } else if (GrabInput) {
            player.FindClosestBox();
            GrabDistance = player.ClosestBoxDistance < playerData.grabDistance;
            if (GrabDistance) { stateMachine.ChangeState(player.GrabState); }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
