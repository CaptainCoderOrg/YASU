using UnityEngine;
using UnityEngine.UI;

public class Toggleable : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LayoutElement _layoutElement;

    [SerializeField] private bool _isVisible;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            HandleChange();
        }
    }

    public bool Toggle() => IsVisible = !IsVisible;

    void Awake()
    {
        HandleChange();
    }

    private void HandleChange()
    {
        _layoutElement.ignoreLayout = !_isVisible;
        _canvasGroup.alpha = _isVisible ? 1 : 0;
        _canvasGroup.blocksRaycasts = _isVisible;
        _canvasGroup.interactable = _isVisible;
    }
}