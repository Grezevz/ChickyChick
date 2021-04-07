﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public GameObject SpriteObject { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField] private Transform groundCheck;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    private int rotatingID, bounceX, bounceY;
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake() {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }

    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        SpriteObject = transform.GetChild(0).gameObject;

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update() {
        CurrentVelocity = RB.velocity;
        Rotate(InputHandler.NormInputX);
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity) {
        // workspace = Vector2.SmoothDamp(CurrentVelocity, new Vector2(velocity, CurrentVelocity.y), ref m_Velocity, 0.05f);
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity) {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Functions
    public bool CheckIfGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) { Flip(); }
    }    
    #endregion

    #region Animation Functions
    public void Rotate(int dir) {
        if (LeanTween.isTweening(rotatingID)) { LeanTween.cancel(rotatingID); }
        rotatingID = LeanTween.rotateZ(SpriteObject, Mathf.Abs(dir)*-playerData.rotation, 0.2f).setEaseOutQuint().id;
    }

    private void CancelSquash() {
        if (LeanTween.isTweening(bounceX)) { LeanTween.cancel(bounceX); }
        if (LeanTween.isTweening(bounceY)) { LeanTween.cancel(bounceY); }
    }

    public void JumpSquash() {
        CancelSquash();

        Vector2 currentScale = SpriteObject.transform.localScale;

        // Squash and Stretch
        float squashAmount = 0.30f;
        bounceX = LeanTween.scaleX(SpriteObject, currentScale.x + squashAmount, 0.1f).setEaseInOutBounce().id;
        bounceY = LeanTween.scaleY(SpriteObject, currentScale.y - (squashAmount/3), 0.1f).setEaseInOutBounce().id;

        StartCoroutine(JumpStretch(currentScale));
    }

    private IEnumerator JumpStretch(Vector2 scale) {
        yield return new WaitForSeconds(0.15f);

        CancelSquash();
        
        bounceX = LeanTween.scaleX(SpriteObject, scale.x, 0.1f).setEaseInOutBounce().id;
        bounceY = LeanTween.scaleY(SpriteObject, scale.y, 0.1f).setEaseInOutBounce().id;
    }

    private void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    #region Other Fuctions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();
    #endregion
}