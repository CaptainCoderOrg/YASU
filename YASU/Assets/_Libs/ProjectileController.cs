using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private SelfDestructSFX _impactSFX;
    [SerializeField] private ProjectileHitTrigger _trigger;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _despawnDistance = 20f;
    [field: SerializeField] public int Damage { get; private set; }= 1;
    [SerializeField] private Animator _animator;

    void OnEnable()
    {
        _trigger.OnHit += HandleHit;
    }

    void OnDisable()
    {
        _trigger.OnHit -= HandleHit;
    }

    private void HandleHit(Collider2D d)
    {
        Instantiate(_impactSFX);
        _animator.SetTrigger("Hit");
        _rigidBody.linearVelocity = Vector2.zero;
        _trigger.OnHit -= HandleHit;
        if (d.TryGetComponent<PlayerHitTrigger>(out var playerHitTrigger))
        {
            playerHitTrigger.Hit(this);
        }
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }

    public virtual void FireTowards(Vector2 start, Vector2 target)
    {
        Vector2 normalized = target - start;
        float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = start;
        _rigidBody.linearVelocity = (target - start).normalized * _projectileSpeed;
    }

    void Update()
    {
        if((Camera.main.transform.position - transform.position).magnitude > _despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}