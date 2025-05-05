using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour
{
    [SerializeField] private CharacterAimController _aimController;
    [SerializeField] private CharacterController2D _characterController;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<WeaponUpgradeController>(out var upgrade))
        {
            upgrade.Collect(_aimController);
        }
        else if (collision.TryGetComponent<ShipPartController>(out var shipPart))
        {
            shipPart.Collect(_characterController);
        }
    }
}