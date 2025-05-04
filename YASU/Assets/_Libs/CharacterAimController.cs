using UnityEngine;

public class CharacterAimController : MonoBehaviour
{

    [SerializeField] private GameObject _aimCrossHair;

    [field: SerializeField] public bool UseMouse { get; set; } = true;

    void Update()
    {
        if (Input.GetMouseButton(0)) { UseMouse = true; }
        AimWithMouse(Input.mousePosition);
    }

    public void AimWithController(Vector2 aimDirection)
    {
        UseMouse = false;
        if (aimDirection == Vector2.zero) { return; }
        Aim(aimDirection);
        
    }

    public void Aim(Vector2 aimDirection)
    {
        _aimCrossHair.transform.localPosition = aimDirection;
    }

    public void AimWithMouse(Vector2 aimDirection)
    {
        if (!UseMouse) { return; }
        Vector2 worldSpace = (Vector2)Camera.main.ScreenToWorldPoint(aimDirection);
        Vector2 target = (worldSpace - (Vector2)transform.position).normalized;
        Aim(target);
    }
}