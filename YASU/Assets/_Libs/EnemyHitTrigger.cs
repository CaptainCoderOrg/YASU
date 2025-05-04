using UnityEngine;

public class EnemyHitTrigger : MonoBehaviour
{
    public event System.Action<ProjectileController> OnHit;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.TryGetComponent<ProjectileController>(out var projectile))
        {
            OnHit?.Invoke(projectile);
        }        
    }
}