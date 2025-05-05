using UnityEngine;

public class SoundCheckController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private bool _first;
    public void SoundCheck()
    {
        if (_first && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        _first = true;
    }
}