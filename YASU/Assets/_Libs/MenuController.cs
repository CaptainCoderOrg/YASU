using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuData _menuData;
    [SerializeField] private Toggleable _toggleable;
    
    void Awake()
    {
        _toggleable.IsVisible = _menuData.IsOpen;
    }

    public void Toggle() => _menuData.IsOpen = _toggleable.Toggle();
}