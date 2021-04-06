using UnityEngine;

public class PlayerJump : StateMachineBehaviour
{
    private CharacterController2D controller;
    private PlayerBase playerbase;
    [SerializeField] private float _moveSpeed = 40f;
    private float _horizontalMove = 0f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller = animator.GetComponent<CharacterController2D>();
        playerbase = animator.GetComponent<PlayerBase>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Check Input Keys
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, true);

        if (controller.m_Grounded) { animator.SetBool("Jump", false); animator.SetBool("Idle", true); }
    }
}
