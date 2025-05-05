using UnityEngine;

public class HealthPill : MonoBehaviour
{
    [SerializeField] private bool _isDamaged;
    [SerializeField] private Animator _animator;
    public bool IsDamaged
    {
        get => _isDamaged;
        set
        {
            if (_isDamaged == value) { return; }
            _isDamaged = value;
            if (_isDamaged) 
            {  
                _animator.SetTrigger("Damage");
            }
            else
            {
                _animator.SetTrigger("Heal");
            }

        }
    }
}