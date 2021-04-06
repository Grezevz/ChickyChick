using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected float startTime;
    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, string _animBoolName) {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.playerData = _playerData;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter() {
            DoChecks();
            player.Anim.SetBool(animBoolName, true);
            startTime = Time.time;
            isAnimationFinished = false;
            Debug.Log(animBoolName);
    }

    public virtual void Exit() => player.Anim.SetBool(animBoolName, false);

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() => DoChecks();

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;
}
