using UnityEngine;

public class BGParallaxController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Material _material;
    [SerializeField] private Vector2 _scrollRate = new (1, 0);
    [SerializeField] private Vector2 _scale = new (0.01f, 0.001f);
    [SerializeField] private Vector2 _offset;

    void Update()
    {
        _material.mainTextureOffset = (((Vector2)_cameraTransform.position)*_scrollRate*_scale) + _offset;
    }
}