using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour
{
    [SerializeField] private CharacterAimController _aimController;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<WeaponUpgradeController>(out var upgrade))
        {
            upgrade.Collect(_aimController);
        }
    }
}