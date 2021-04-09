using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;
    
    // Checks
    private bool isGrounded;
    private bool isJumping;
    private bool coyoteTime;

    public PlayerInAirState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName) {}

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

        CheckCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;

        CheckJumpMultipler();

        if (isGrounded && player.CurrentVelocity.y < 0.01f) { stateMachine.ChangeState(player.LandState); return; }
        if (jumpInput && player.JumpState.CanJump()) { stateMachine.ChangeState(player.JumpState); return; }
        if (dashInput && player.DashState.CheckIfCanDash()) { stateMachine.ChangeState(player.DashState); return; }

        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(playerData.movementVelocity * xInput);

        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
    }

    private void CheckJumpMultipler() {
        if (isJumping) {
            if (jumpInputStop) {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            } else if (player.CurrentVelocity.y <= 0) {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime() {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime) {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void SetIsJumping() => isJumping = true;
}
