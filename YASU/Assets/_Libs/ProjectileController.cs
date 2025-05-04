using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _despawnDistance = 20f;

    public void FireTowards(Vector2 start, Vector2 target)
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