using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] private ParticleSystem[] _dustPS;

    // Grab Variables
    [System.NonSerialized] public Transform _grabObject;

    public void CreateDust() { 
        foreach (ParticleSystem _dust in _dustPS) { _dust.Play(); }
    }

    public float Normalize(float num) {
        num = Mathf.Round(num * 100);

        if (num > 0) { return (num / num); }
        if (num < 0) { return -1*(num / num); }
        return 0;
    }
    
    // Check if in range to grab Box
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "BoxCollider") { _grabObject = other.transform.parent.transform; }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "BoxCollider") { _grabObject = null; }
    }
}