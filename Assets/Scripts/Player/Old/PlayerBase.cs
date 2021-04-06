using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _dustPS;
    [System.NonSerialized] public Transform _grabObject;

    public void CreateDust() { foreach (ParticleSystem _dust in _dustPS) { _dust.Play(); } }

    
    // Check if in range to grab Box
    private void OnTriggerEnter2D(Collider2D other) { if (other.gameObject.tag == "BoxCollider") { _grabObject = other.transform.parent.transform; } }
    private void OnTriggerExit2D(Collider2D other) { if (other.gameObject.tag == "BoxCollider") { _grabObject = null; } }
}