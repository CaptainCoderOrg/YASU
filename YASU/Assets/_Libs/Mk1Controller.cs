using System.Collections;
using UnityEngine;

public class Mk1Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private bool _isActive;
    [SerializeField] private float _activationDistance = 20;
    [SerializeField] private ProjectileController _projectile;
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private Transform _projectileAim;
    private Coroutine _running;
    [SerializeField] private float _chargeDelay = 1f;
    [SerializeField] private float _fireDelay = 2f;
    private WaitForSeconds _waitForCharge;
    [SerializeField] private Animator _animator;
    private WaitForSeconds _waitForFire;
    [SerializeField] private float _move = 0;
    [SerializeField] private float _speed = 2f;


    void Awake()
    {
        _waitForCharge = new WaitForSeconds(_chargeDelay);
        _waitForFire = new WaitForSeconds(_fireDelay);
    }
    

    void Update()
    {
        if(Vector2.Distance(Camera.main.transform.position, transform.position) < _activationDistance)
        {
            _isActive = true;
        }

        if (!_isActive) { return; }
        if (_running != null) { return; }

        _running = StartCoroutine(Activate());

    }

    void FixedUpdate()
    {
        Vector2 target = Vector2.MoveTowards(transform.position, transform.position + transform.right, _move*_speed*Time.fixedDeltaTime);       
        _rigidBody.MovePosition(target);
    }

    private IEnumerator Activate()
    {
        while (true)
        {
            _move = 0;
            _animator.SetTrigger("Charge");
            yield return _waitForCharge;
            _animator.SetTrigger("Fire");
            ProjectileController newProjectile = Instantiate(_projectile);
            newProjectile.FireTowards(_projectileSpawn.transform.position, _projectileAim.transform.position);
            _animator.SetTrigger("MoveLeft");
            yield return _waitForFire;
            _move = 0;
            _animator.SetTrigger("Charge");
            yield return _waitForCharge;
            _animator.SetTrigger("Fire");
            newProjectile = Instantiate(_projectile);
            newProjectile.FireTowards(_projectileSpawn.transform.position, _projectileAim.transform.position);
            _animator.SetTrigger("MoveRight");
            yield return _waitForFire;
        }
    }

    public void Move(float direction) => _move = direction;

    
}