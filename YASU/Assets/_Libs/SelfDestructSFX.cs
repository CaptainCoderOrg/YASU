using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SelfDestructSFX : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip[] _clips;
    public UnityEvent OnDestruct;
    void Awake()
    {
        _source = GetComponent<AudioSource>();
        if (_clips != null && _clips.Length > 0)
        {
            _source.clip = _clips[Random.Range(0, _clips.Length)];
            _source.Play();
        }
        transform.parent = null;
        StartCoroutine(DestroyWhenDone());
    }

    private IEnumerator DestroyWhenDone()
    {
        yield return null;
        while (_source.isPlaying) { yield return null; }
        OnDestruct?.Invoke();
        Destroy(gameObject);
    }
}