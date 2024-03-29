﻿public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName) {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0) { stateMachine.ChangeState(player.MoveState); }
        else if (isAnimationFinished) { stateMachine.ChangeState(player.IdleState); }
    }
}
