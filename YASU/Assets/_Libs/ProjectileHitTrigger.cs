using UnityEngine;

public class ProjectileHitTrigger : MonoBehaviour
{
    public event System.Action<Collider2D> OnHit;

    void OnTriggerEnter2D(Collider2D collision) => OnHit?.Invoke(collision);
}