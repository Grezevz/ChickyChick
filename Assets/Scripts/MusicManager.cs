using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private string _songName = "PuzzleMusic";
    [Range(0f, 15f)][SerializeField] private float _maxBreak = 2f;
    private float _breakTimer;
    private AudioManager _audioManager;

    private void Start() { 
        _audioManager = GetComponent<AudioManager>();
    }

    private void Update() {
        if (_audioManager.isPlaying(_songName)) { return; }

        if (_breakTimer < _maxBreak) { _breakTimer += Time.deltaTime; }
        else { _audioManager.Play(_songName); _breakTimer = 0; }
    }
}
