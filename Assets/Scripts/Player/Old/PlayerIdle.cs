﻿using UnityEngine;

public class PlayerIdle : StateMachineBehaviour
{
    private CharacterController2D controller;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => controller = animator.GetComponent<CharacterController2D>();

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bool _jump = Input.GetButtonDown("Jump");
        bool moveCheck = _jump || Input.GetAxisRaw("Horizontal") != 0;
        if (moveCheck) { animator.SetBool("Idle", false); }
        controller.Move(0, _jump, false);
        if (_jump) { animator.SetBool("Jump", true); }
    }
}
