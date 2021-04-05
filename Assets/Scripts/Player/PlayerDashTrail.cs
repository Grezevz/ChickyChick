using UnityEngine;

public class PlayerDashTrail : MonoBehaviour
{
    private Color _alpha;
    // Start is called before the first frame update
    void Start() {
        _alpha = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update() {
        if (_alpha.a < 0) { Destroy(gameObject); }
        
        _alpha.a -= 0.075f;
        gameObject.GetComponent<SpriteRenderer>().color = _alpha;
    }
}
