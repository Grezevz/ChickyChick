using UnityEngine;

public class PlayerGrab : StateMachineBehaviour
{   
    private CharacterController2D controller;
    private PlayerBase playerbase;

    // Movement
    [SerializeField] private float runSpeed = 25f;
    private float _horizontalMove = 0f;

    // Objects and Size
    private Transform box;

    private float width;
    private float direction;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller = animator.GetComponent<CharacterController2D>();
        playerbase = animator.GetComponent<PlayerBase>();
        box = playerbase._grabObject;

        // Reset Check
        if (box == null) { animator.SetBool("GrabKey", false); return; }
        float posCheck = Mathf.Sign(box.position.x - animator.transform.position.x); 
        float scaleCheck = Mathf.Sign(animator.transform.localScale.x);
        if (posCheck != scaleCheck) { animator.SetBool("GrabKey", false); }

        // Sprite Size and Direction
        float boxSize = box.GetComponent<SpriteRenderer>().bounds.size.x;
        float spriteSize = animator.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        width = (boxSize / 2) + (spriteSize / 2) - 1.05f;
        direction = Mathf.Sign(box.transform.position.x - animator.transform.position.x);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { 
        box = playerbase._grabObject;
        if (Input.GetButtonUp("Grab") || box == null) { animator.SetBool("GrabKey", false); }

        // Get and Set Position
        Vector3 pos = animator.transform.position;
        box.LeanMoveX(pos.x + (width*direction), 0f);

        // Check Basic Movement
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
