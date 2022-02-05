using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Player player;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        cam = Camera.main;   
    }

    private void Update()  {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }
    public void OnMoveInput (InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    #region Jump Input
    public void OnJumpInput(InputAction.CallbackContext context) {
        if (context.started) { 
            JumpInput = true; 
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled) { JumpInputStop = true; }
    }

    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime() {
        if (Time.time >= jumpInputStartTime + inputHoldTime) { JumpInput = false; }
    }
    #endregion
    
    #region Dash Input
    public void OnGrabInput(InputAction.CallbackContext context) {
        if (context.started) { GrabInput = true; }
        if (context.canceled) { GrabInput = false; }
    }

    public void OnDashInput(InputAction.CallbackContext context) {
        if (context.started) {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }

        if (context.canceled) { DashInputStop = true; }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context) {
        RawDashDirectionInput = context.ReadValue<Vector2>();
        
        Vector3 pos = transform.position + new Vector3(0f, player.SpriteHeight/2);
        // RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - pos;
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void UseDashInput() => DashInput = false;
    
    private void CheckDashInputHoldTime() {
        if (Time.time > dashInputStartTime + inputHoldTime) { DashInput = false; } 
    }
    #endregion

}
