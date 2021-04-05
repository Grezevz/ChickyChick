using UnityEngine;
public class CoinMovement : MonoBehaviour
{
    // Parent
    [SerializeField] ExpandingLightPoint _parent;

    // Animation Variables
    [SerializeField] float animationTime = 3f;
    [SerializeField] private Transform target;
    [SerializeField] private float idleHeight = 1f;
    private bool isCollected = false;
    private Vector2 destination;

    private void Awake() {
        float yy = transform.position.y;
        LeanTween.moveY(transform.gameObject, yy-idleHeight, Random.Range(1.5f, 2.5f)).setEaseInOutSine().setLoopPingPong();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            if (!isCollected) {
                FindObjectOfType<AudioManager>().PlayOneShot("Energy");
                CoinCollected();
            }
        }
    }

    private void Update() {
        if (Vector3.Distance(transform.position, destination) < 0.1f) { Destroy(gameObject); }
    }

    private void CoinCollected() {
        if (!isCollected) {
            isCollected = true;
            _parent.ShowLight();
            destination = new Vector2(target.position.x, target.position.y);

            // Transform
            LeanTween.moveX(transform.gameObject, destination.x, animationTime).setEaseInBack();
            LeanTween.moveY(transform.gameObject, destination.y, animationTime).setEaseOutBack();

            // Scale and Rotate
            LeanTween.scale(transform.gameObject, new Vector3(2.5f, 2.5f, 2.5f), animationTime).setEaseInQuart();
            LeanTween.rotateZ(transform.gameObject, 2000, animationTime).setEaseInCubic();
        }
    }
}
