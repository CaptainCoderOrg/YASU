using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitTrigger : MonoBehaviour
{
    [field: SerializeField] public int MaxHealth { get; private set; } = 5;
    public int Health => MaxHealth - Damage;
    
    [SerializeField] private int _damage;
    public int Damage
    {
        get => _damage;
        set
        {
            int previous = Health;
            _damage = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChanged?.Invoke(Health);
            if (previous > Health)
            {
                OnDamaged?.Invoke();
            }
            if (previous < Health)
            {
                OnHealed?.Invoke();
            }
        }
    }
    [field: SerializeField] public UnityEvent<int> OnHealthChanged { get; private set; }
    [field: SerializeField] public UnityEvent OnDamaged { get; private set; }
    [field: SerializeField] public UnityEvent OnHealed { get; private set; }
    [SerializeField] private CharacterAimController _aimController;
    [SerializeField] private CharacterController2D _characterController;
    [SerializeField] private PlayerInputController _inputController;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Collider2D _hitbox;
    [SerializeField] private Collider2D _footbox;
    [SerializeField] private Animator _deathAnimator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LevelController _levelController;
    bool _isDead = false;

    void Awake()
    {
        _levelController = FindFirstObjectByType<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) { return; }
        if (collision.TryGetComponent<WeaponUpgradeController>(out var upgrade))
        {
            upgrade.Collect(_aimController);
        }
        else if (collision.TryGetComponent<ShipPartController>(out var shipPart))
        {
            shipPart.Collect(_characterController, this, _levelController);
        }
    }

    public void DisablePlayer()
    {
        _isDead = true;
        _aimController.gameObject.SetActive(false);
        _inputController.gameObject.SetActive(false);
        _characterController.enabled = false;
        _hitbox.gameObject.SetActive(false);
        _footbox.gameObject.SetActive(false);
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Death()
    {
        Damage = MaxHealth;
        DisablePlayer();
        _spriteRenderer.enabled = false;
        _deathAnimator.gameObject.SetActive(true);
        
    }

    internal void Hit(ProjectileController projectileController)
    {
        if (_isDead) { return; }
        Damage += projectileController.Damage;
        if (Health <= 0)
        {
            Death();
        }
    }

    void Update()
    {
        if (transform.position.y < -1000)
        {
            Death();
        }
    }
}