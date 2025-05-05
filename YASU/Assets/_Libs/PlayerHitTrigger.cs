using System;
using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour
{
    [field: SerializeField] public float Health { get; private set; } = 5;
    [field: SerializeField] public float Damage { get; private set; } = 0;
    [SerializeField] private CharacterAimController _aimController;
    [SerializeField] private CharacterController2D _characterController;
    [SerializeField] private PlayerInputController _inputController;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Collider2D _hitbox;
    [SerializeField] private Collider2D _footbox;
    [SerializeField] private Animator _deathAnimator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LevelController _levelController;

    void Awake()
    {
        _levelController = FindFirstObjectByType<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<WeaponUpgradeController>(out var upgrade))
        {
            upgrade.Collect(_aimController);
        }
        else if (collision.TryGetComponent<ShipPartController>(out var shipPart))
        {
            shipPart.Collect(_characterController, _levelController);
        }
    }

    public void DisablePlayer()
    {
        _aimController.gameObject.SetActive(false);
        _inputController.gameObject.SetActive(false);
        _characterController.enabled = false;
        _hitbox.gameObject.SetActive(false);
        _footbox.gameObject.SetActive(false);
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Death()
    {
        DisablePlayer();
        _spriteRenderer.enabled = false;
        _deathAnimator.gameObject.SetActive(true);

    }

    internal void Hit(ProjectileController projectileController)
    {
        Damage += projectileController.Damage;
        if (Damage >= Health)
        {
            Death();
        }
    }
}