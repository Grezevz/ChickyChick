using UnityEngine;

public class PlayerMove : StateMachineBehaviour
{
    private CharacterController2D controller;
    private PlayerBase pb;

    // Movement
    [SerializeField] private float _runSpeed = 40f;
    [SerializeField] private float _jumpHeight = 900f;
    private float _horizontalMove = 0f;
    private bool _jump = false;

    // Access Controller
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller = animator.GetComponent<CharacterController2D>();
        controller.m_JumpForce = _jumpHeight;
        pb = animator.GetComponent<PlayerBase>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Switch State
        if (controller.m_Grounded) {
            if (Input.GetButtonDown("Dash")) { animator.SetTrigger("DashKey"); }
            if (Input.GetButton("Grab") && pb._grabObject) { animator.SetBool("GrabKey", true); }
        }

        // Check Input Keys
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
        if (Input.GetButtonDown("Jump")) { _jump = true; }
       
        // Move and Reset Jump
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump, true);
        _jump = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.ResetTrigger("DashKey");
    }

}
