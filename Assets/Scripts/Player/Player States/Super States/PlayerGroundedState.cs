public class PlayerGroundedState : PlayerState
{
    // Inputs
    protected int xInput;
    protected bool GrabInput;
    private bool jumpInput;
    private bool dashInput;

    // Check
    protected bool GrabDistance;
    protected bool GrabDistanceRelease;
    private bool isGrounded;
    
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName) {}

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        GrabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;

        if (jumpInput && player.JumpState.CanJump()) { 
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState); 
        } else if (!isGrounded) { 
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState); 
        } else if (GrabInput) {
            player.FindClosestBox();
            GrabDistance = player.ClosestBoxDistance < playerData.grabDistance;
            if (GrabDistance) { stateMachine.ChangeState(player.GrabState); }
        } else if (dashInput && player.DashState.CheckIfCanDash()) { stateMachine.ChangeState(player.DashState); return; }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
