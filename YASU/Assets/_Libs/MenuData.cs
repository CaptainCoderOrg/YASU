using UnityEngine;

[CreateAssetMenu(menuName = "MenuData")]
public class MenuData : ScriptableObject
{
    [field: SerializeField] public bool IsOpen { get; set; }
    void OnEnable()
    {
        IsOpen = true;
    }
}