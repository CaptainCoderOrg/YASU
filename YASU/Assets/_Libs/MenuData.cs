using UnityEngine;

[CreateAssetMenu(menuName = "MenuData")]
public class MenuData : ScriptableObject
{
    [field: SerializeField] public bool IsOpen { get; set; }
    void OnEnable()
    {
#if UNITY_EDITOR
#else
        IsOpen = true;
#endif
    }
}