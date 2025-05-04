using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraHelper : MonoBehaviour
{
    [SerializeField] private CinemachineFollow _follow;
    [SerializeField] private CharacterController2D _player;
    [SerializeField] private float _cameraLead = 7;
    private float _targetXOffset = 0;
    [SerializeField] private float _changeDuration = 2f;

    void Update()
    {
        // if (_player.IsFlipped)
        float targetX = _cameraLead * (_player.IsFlipped ? -1 : 1);
        if (_targetXOffset != targetX)
        {
            StopAllCoroutines();
            _targetXOffset = targetX;
            StartCoroutine(TweenOffset());
        }
    }

    private IEnumerator TweenOffset()
    {
        Vector3 start = _follow.FollowOffset;
        Vector3 end = _follow.FollowOffset;
        end.x = _targetXOffset;
        float elapsed = 0;
        while (elapsed < _changeDuration)
        {
            _follow.FollowOffset = Vector3.Lerp(start, end, elapsed/_changeDuration);
            yield return null;
            elapsed += Time.deltaTime;
        }
        _follow.FollowOffset = end;
    }
}