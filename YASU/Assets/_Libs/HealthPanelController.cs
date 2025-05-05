using UnityEngine;

public class HealthPanelController : MonoBehaviour
{
    [SerializeField] private HealthPill[] _healthPills;

    public void SetHealth(int health)
    {
        int ix = 0;
        for (; ix < health; ix++)
        {
            _healthPills[ix].IsDamaged = false;
        }
        for (; ix < _healthPills.Length; ix++)
        {
            _healthPills[ix].IsDamaged = true;
        }
    }
}