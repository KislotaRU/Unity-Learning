using UnityEngine;

public class MovingToWaypoints : MonoBehaviour
{
    [SerializeField] private Transform _parentWaypoints;
    [SerializeField] private float _speedMovement;

    private Transform[] _waypoints;
    private int _currentWaypoint = 0;

    private Vector3 CurrentWaypointPosition => _waypoints[_currentWaypoint].position;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (_waypoints == null)
            return;

        Move();
    }

    private void Initialize()
    {
        if (_parentWaypoints == null)
            return;

        _waypoints = new Transform[_parentWaypoints.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = _parentWaypoints.GetChild(i);
    }

    private void Move()
    {
        if (transform.position == CurrentWaypointPosition)
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        transform.position = Vector3.MoveTowards(transform.position, CurrentWaypointPosition, _speedMovement * Time.deltaTime);
    }
}