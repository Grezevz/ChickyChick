using UnityEngine;

public class PlayerGrab : StateMachineBehaviour
{   
    private CharacterController2D controller;
    private PlayerBase pb;
    private Transform box;
    private SpriteRenderer boxSpr;
    private SpriteRenderer spriteSpr;
    private float width;
    // Movement
    [SerializeField] private float runSpeed = 25f;
    private float _horizontalMove = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller = animator.GetComponent<CharacterController2D>();
        pb = animator.GetComponent<PlayerBase>();
        if (!pb._grabObject) { animator.SetBool("GrabKey", false); }

        // Sprite Width
        box = pb._grabObject;
        boxSpr = box.GetComponent<SpriteRenderer>();
        spriteSpr = animator.GetComponentInChildren<SpriteRenderer>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { 
        box = pb._grabObject;
        if (Input.GetButtonUp("Grab") || box == null) { animator.SetBool("GrabKey", false); }

        // Check Rotation
        float posCheck = Mathf.Sign(box.position.x - animator.transform.position.x); 
        float scaleCheck = Mathf.Sign(animator.transform.localScale.x);
        if (posCheck != scaleCheck) { controller.Flip(scaleCheck * -1); }

        // Get and Set Position
        Vector3 pos = animator.transform.position;
        width = (boxSpr.bounds.size.x / 2) + (spriteSpr.bounds.size.x / 2) - 1.05f;
        float dir = Mathf.Sign(boxSpr.transform.position.x - spriteSpr.transform.position.x);
        box.position = new Vector3(pos.x + (width*dir), box.position.y, box.position.z);

        // Check Basic Movement
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
