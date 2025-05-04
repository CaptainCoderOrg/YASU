using UnityEngine;

public class WaypointTraveler : MonoBehaviour
{
    const float EPSILON = 0.17f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _waypointTransform;
    [SerializeField] private Vector2[] _waypoints;
    [SerializeField] private int _ix;
    [SerializeField] private Rigidbody2D _rigidBody;
    public Vector2 Target => _waypoints[_ix];
    public float Distance => (Target - (Vector2)transform.position).magnitude;
    [field: SerializeField] public float Speed { get; set; } = 2;

    [field: SerializeField] public float LastMove { get; set; }

    void Awake()
    {
        InitWaypoints();
    }

    public void InitWaypoints()
    {
        _waypoints = new Vector2[_waypointTransform.childCount];
        for (int ix = 0; ix < _waypointTransform.childCount; ix++)
        {
            _waypoints[ix] = _waypointTransform.GetChild(ix).transform.position;
        }
    }

    void FixedUpdate()
    {
        Vector2 target = Vector2.MoveTowards(transform.position, Target, Speed*Time.fixedDeltaTime);       
        _rigidBody.MovePosition(target);

        if ((Target - (Vector2)transform.position).magnitude <= EPSILON)
        {
            _ix++;
            if (_ix >= _waypoints.Length)
            {
                _ix = 0;
            }
        }

        LastMove = ((Vector2)transform.position - target).x;
        _spriteRenderer.flipX = LastMove < 0;
    }
}