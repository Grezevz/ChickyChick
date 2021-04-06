using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[SerializeField] private float _rotation = 10f;				// Rotation Value when Moving
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[System.NonSerialized] public bool m_Grounded;            	// Whether or not the player is grounded.
	const float k_GroundedRadius = .2f; 						// Radius of the overlap circle to determine if grounded
	private Vector3 m_Velocity = Vector3.zero;
	private float m_Direction;								// Player Sprite Direction
	private Vector3 _startScale;							// Player Sprite Start Scale

	private Rigidbody2D m_Rigidbody2D;
	private Transform m_Transform;
	private PlayerBase m_PlayerBase;
	private GameObject m_PlayerSprite;


	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		// Get Components
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Transform = GetComponent<Transform>();
		m_PlayerBase = GetComponent<PlayerBase>();
		m_PlayerSprite = m_Transform.GetChild(0).gameObject;

		// Set Variables
		_startScale = m_PlayerSprite.transform.localScale;
		m_Direction = Mathf.Sign(m_Transform.localScale.x);
        _direction = m_PlayerBase.Normalize(transform.position.x - _previousPos);
		_previousPos = m_Transform.position.x;

		if (OnLandEvent == null) { OnLandEvent = new UnityEvent(); }
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				m_Grounded = true;
				if (!wasGrounded) {
						OnLandEvent.Invoke();
						m_PlayerBase.CreateDust();	
				}
			}
		}
	}

	public void Move(float move, bool jump, bool turn)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl) {
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// Flip character if looking in opposite direction
			if (turn && move != 0) {
				if (Mathf.Sign(move) != Mathf.Sign(m_Transform.localScale.x)) { Flip(Mathf.Sign(move)); }
			}

			// Move Rotation
			Rotate(m_PlayerBase.Normalize(m_Transform.position.x - _previousPos));
			_previousPos = m_Transform.position.x;
		}

		// If the player should jump...
		// SquashStretch(jump);
		if (m_Grounded && jump) {
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}
	
	public void Flip(float dir) {
		// LeanTween.scaleX(m_Transform.gameObject, m_Transform.localScale.x * -1, 0.01f);
		float xScale = m_Transform.localScale.x;
		if (dir > 0) {
			m_Transform.localScale = new Vector2(Mathf.Abs(m_Transform.localScale.x), m_Transform.localScale.y);
		} else if (dir < 0) {
			m_Transform.localScale = new Vector2(Mathf.Abs(m_Transform.localScale.x) * -1, m_Transform.localScale.y);
		}
	}

	// Rotation Variables
    private int _rotatingID;
    private float _direction;
	private float _previousPos;

	public void Rotate(float dir) {
        if (_direction == dir) { return; }
        _direction = dir;

        switch (dir) {
            case -1:
                if (LeanTween.isTweening(_rotatingID)) { LeanTween.cancel(_rotatingID); }
                _rotatingID = LeanTween.rotateZ(m_PlayerSprite, _rotation, 0.2f).setEaseOutQuint().id;
                break;
            case 0:
                // Cancel Previous Rotation and Rotate
                if (LeanTween.isTweening(_rotatingID)) { LeanTween.cancel(_rotatingID); }
                _rotatingID = LeanTween.rotateZ(m_PlayerSprite, 0, 0.1f).setEaseInQuint().id;
                break;
            
            case 1:
                if (LeanTween.isTweening(_rotatingID)) { LeanTween.cancel(_rotatingID); }
                _rotatingID = LeanTween.rotateZ(m_PlayerSprite, -_rotation, 0.2f).setEaseOutQuint().id;
                break;
        }

    }
	private int bounceTimer;
    private int bounceX, bounceY;
    public void SquashStretch(bool _jump) {
        // Squash and Stretch
        float dir = m_Direction;
		Vector3 _currentScale = m_PlayerSprite.transform.localScale;
        
		if(_jump && m_Grounded) {
            // Cancel Resetting Scale
            if (LeanTween.isTweening(bounceX)) { LeanTween.cancel(bounceX); }
            if (LeanTween.isTweening(bounceY)) { LeanTween.cancel(bounceY); }

            // Squash and Stretch
            float squashAmount = 0.45f;
            bounceX = LeanTween.scaleX(m_PlayerSprite, _currentScale.x + (squashAmount * dir), 0.035f).setEaseInOutBounce().id;
            bounceY = LeanTween.scaleY(m_PlayerSprite, _currentScale.y - 0.15f, 0.035f).setEaseInOutBounce().id;
            bounceTimer = 0;
        }
        
        // Reset Sprite
        if (bounceTimer > 15) {
            if (_currentScale != _startScale) {
                // Cancel Previous Squash
                if (LeanTween.isTweening(bounceX)) { LeanTween.cancel(bounceX); }
                if (LeanTween.isTweening(bounceY)) { LeanTween.cancel(bounceY); }

                // Reset Back to Normal Scale
                bounceX = LeanTween.scaleX(m_PlayerSprite, _startScale.x*dir, 0.035f).setEaseInOutBounce().id;
                bounceY = LeanTween.scaleY(m_PlayerSprite, _startScale.y, 0.035f).setEaseInOutBounce().id;
            }
        } else { bounceTimer++; }
    }
	
}