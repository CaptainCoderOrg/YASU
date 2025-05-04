using UnityEngine;

public class CharacterAimController : MonoBehaviour
{

    [SerializeField] private float _crossHairDistance = 5;
    [SerializeField] private GameObject _aimCrossHair;
    public Vector2 CrossHairPosition => _aimCrossHair.transform.localPosition;

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
        _aimCrossHair.transform.localPosition = aimDirection * _crossHairDistance;
    }

    public void AimWithMouse(Vector2 aimDirection)
    {
        if (!UseMouse) { return; }
        Vector2 worldSpace = (Vector2)Camera.main.ScreenToWorldPoint(aimDirection);
        Vector2 target = (worldSpace - (Vector2)transform.position).normalized;
        Aim(target);
    }
}