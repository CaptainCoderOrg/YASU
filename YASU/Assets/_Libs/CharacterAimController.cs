using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterAimController : MonoBehaviour
{
    [SerializeField] private ProjectileController _basicProjectilePrefab;
    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private int _ammo = 0;
    public int Ammo
    {
        get => _ammo;
        private set
        {
            if (_ammo == value) { return; }
            _ammo = Mathf.Clamp(value, 0, 30);
            OnAmmoChanged?.Invoke(_ammo);
        }
    }
    [field: SerializeField] public UnityEvent<int> OnAmmoChanged;
    [SerializeField] private float _crossHairDistance = 5;
    [SerializeField] private GameObject _aimCrossHair;
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private float _spawnDistance = 1.5f;
    public Vector2 CrossHairPosition => _aimCrossHair.transform.localPosition;
    
    
    public void CollectProjectile(ProjectileController projectile, int ammo)
    {
        _weaponIcon.sprite = projectile.HudIcon;
        _projectilePrefab = projectile;
        Ammo = ammo;
    }

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
        _spawnPosition.transform.position = Vector2.MoveTowards(transform.position, _aimCrossHair.transform.position, _spawnDistance);
    }

    public void AimWithMouse(Vector2 aimDirection)
    {
        if (!UseMouse) { return; }
        Vector2 worldSpace = (Vector2)Camera.main.ScreenToWorldPoint(aimDirection);
        Vector2 target = (worldSpace - (Vector2)transform.position).normalized;
        Aim(target);
    }

    internal void Fire()
    {
        Ammo = Mathf.Max(0, Ammo - 1);
        ProjectileController toSpawn = Ammo > 0 ? _projectilePrefab : _basicProjectilePrefab;
        ProjectileController projectile = Instantiate(toSpawn);
        if (projectile.StickToParent)
        {
            projectile.transform.parent = _spawnPosition.transform;
        }
        projectile.FireTowards(_spawnPosition.transform.position, _aimCrossHair.transform.position);
    }
}