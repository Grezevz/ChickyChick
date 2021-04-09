using UnityEngine;

public class PlayerGrabState : PlayerGroundedState
{
    private Transform box;
    private BoxCollider2D boxCollider;
    private float width;
    private float direction;

    public PlayerGrabState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) : base(_player, _stateMachine, _playerData, _animBoolName) {}

    public override void Enter()
    {
        base.Enter();

        CheckDirection();
        SetWidth();
        boxCollider = box.GetComponent<BoxCollider2D>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(playerData.grabMovementVelocity * xInput);
        if (!GrabInput || CheckDistance()) { stateMachine.ChangeState(player.IdleState); }

        SetBoxPosition();
    }

    private bool CheckDistance()
    {
        player.DistanceToBox();
        return player.ClosestBoxDistance > playerData.grabDistanceRelease;
    }

    private void CheckDirection(){
        box = player.ClosestBox;
        direction = Mathf.Sign(box.position.x - player.transform.position.x);
        if (direction != player.FacingDirection) { stateMachine.ChangeState(player.IdleState); }
    }

    private void SetWidth() {
        float boxWidth = box.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float spriteWidth = player.SpriteHeight / 2;
        width = boxWidth + spriteWidth - 1.05f;
    }

    private void SetBoxPosition() {
        Vector2 pos = player.transform.position;
        box.position = new Vector2(pos.x + (width*direction), box.position.y);
    }
}
