using System.Collections;
using UnityEngine;

public class Mk2Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private bool _isActive;
    [SerializeField] private float _activationDistance = 20;
    [SerializeField] private ProjectileController _projectile;
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private Transform[] _projectilePositions;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _move = 0;
    [SerializeField] private float _speed = 5f;
    

    void Update()
    {
        if(Vector2.Distance(Camera.main.transform.position, transform.position) < _activationDistance && !_isActive)
        {
            _isActive = true;
            StartMove();
        }

    }

    void FixedUpdate()
    {
        Vector2 target = Vector2.MoveTowards(transform.position, transform.position + transform.right, _move*_speed*Time.fixedDeltaTime);       
        _rigidBody.MovePosition(target);
    }

    public void StartAttack()
    {
        _move = 0;
        _animator.SetTrigger("Fire");
    }

    public void PerformAttack()
    {
        foreach (Transform t in _projectilePositions)
        {
            ProjectileController p = Instantiate(_projectile);
            p.FireTowards(_projectileSpawn.position, t.position);
        }
    }

    public void StartMove()
    {
        _animator.SetTrigger("Move");
    }

    public void Move(float direction) => _move = direction;

    
}