using UnityEngine;

public class WeaponUpgradeController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectile;
    [SerializeField] private int _ammo = 20;
    [SerializeField] private SelfDestructSFX _collectSound;

    public void Collect(CharacterAimController characterAim)
    {
        characterAim.CollectProjectile(_projectile, _ammo);
        Destroy(gameObject);
        Instantiate(_collectSound);
    }
}