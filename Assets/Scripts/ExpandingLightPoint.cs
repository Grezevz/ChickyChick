using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExpandingLightPoint : MonoBehaviour
{
    [SerializeField] Light2D _light;
    [SerializeField] Color _lightColor;
    [SerializeField] Color _childColor;

    // Child Variables
    SpriteRenderer _egg;
    Light2D _eggLight;

    // Expanding Light Variables
    bool _eggCollected = false;
    [SerializeField] float _maxRadius = 40f;

    private void Awake() {
        // Get Compononent
        _egg = transform.Find("Coin").GetComponent<SpriteRenderer>();
        _eggLight = _egg.transform.Find("Glow").GetComponent<Light2D>();

        _light.color = _lightColor; 
        _eggLight.color = _childColor;
    }

    // Update is called once per frame
    void Update() {
        if (_eggCollected) {
            if (_light.pointLightOuterRadius < _maxRadius) {
                float _changeSpeed = 0.1f;
                _light.pointLightOuterRadius += _changeSpeed;
                _light.pointLightInnerRadius += _changeSpeed / 3;
            }
        }
    }

    public void ShowLight() {
        _light.enabled = true;
        _eggCollected = true;
        
    }
}
