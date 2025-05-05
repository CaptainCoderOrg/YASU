using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyHitTrigger _hitTrigger;
    [SerializeField] private float _health = 1f;
    [SerializeField] private float _damage = 0f;
    private RandomDropController _dropController;

    void OnEnable()
    {
        _hitTrigger.OnHit += HandleHit;
        _dropController = GetComponent<RandomDropController>();
    }

    void OnDisable()
    {
        _hitTrigger.OnHit -= HandleHit;
    }

    private void HandleHit(ProjectileController controller)
    {
        _damage += controller.Damage;
        if (_damage >= _health)
        {
            Destroy(gameObject);
            _dropController?.Drop();
        }
    }
}