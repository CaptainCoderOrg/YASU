using UnityEngine;

public class TripleShotSpawner : ProjectileController
{
    [SerializeField] private ProjectileController _baseProjectile;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform[] _endPositions;

    public override void FireTowards(Vector2 start, Vector2 target)
    {
        Vector2 normalized = target - start;
        float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = start;
        foreach (Transform t in _endPositions)
        {
            ProjectileController projectile = Instantiate(_baseProjectile);
            projectile.FireTowards(_startPosition.transform.position, t.transform.position);
        }
        Destroy(gameObject);
    }
}