using System;
using UnityEngine;

public class PlayerDash : StateMachineBehaviour
{
    [SerializeField] private float _dashSpeed = 120f;
    [SerializeField] private float _dashLength = 1f;
    public GameObject _dashTrail;
    private float _dashTimer, _direction;
    private CharacterController2D controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller = animator.GetComponent<CharacterController2D>();
        _dashTimer = _dashLength;

        // Set Direction First Based on Input, then Based on Previous Direction
        _direction = Mathf.Sign(animator.transform.localScale.x);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if ((_direction == 0) || (_dashTimer <= 0)) { animator.SetTrigger("DashFinished"); return; }
        
        controller.Move((_direction * _dashSpeed) * Time.fixedDeltaTime, false, false);
        _dashTimer -= Time.fixedDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) { animator.ResetTrigger("DashFinished"); }

    // private void CreateTrail(Animator animator) {
    //     Vector3 _pos = animator.transform.position;
    //     Vector3 _scale = animator.transform.localScale;
    //     Quaternion _rotation = animator.transform.rotation;

    //     Instantiate(_dashTrail, _pos, animator.transform.GetChild(0).transform.rotation);
    //     _dashTrail.transform.localScale = _scale;
    //     _dashTrail.transform.rotation = _rotation;
    // }
}
