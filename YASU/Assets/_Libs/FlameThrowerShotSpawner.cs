using System.Collections;
using UnityEngine;

public class FlameThrowerShotSpawner : ProjectileController
{
    [SerializeField] private ProjectileController _baseProjectile;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform[] _endPositions;
    private YieldInstruction _delay;

    void Awake()
    {
        _delay = new WaitForSeconds(1f/24f);
    }

    public override void FireTowards(Vector2 start, Vector2 target)
    {
        StartCoroutine(FireCoroutine(start, target));
    }

    private IEnumerator FireCoroutine(Vector2 start, Vector2 target)
    {
        Vector2 normalized = target - start;
        float angle = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = start;
        for (int i = 0; i < 6; i++)
        {
            Transform t = _endPositions[Random.Range(0, _endPositions.Length)];
            ProjectileController projectile = Instantiate(_baseProjectile);
            projectile.FireTowards(_startPosition.transform.position, t.transform.position);
            projectile.Attach(transform.parent);
            yield return _delay;
        }       
         
        Destroy(gameObject);
    }
}