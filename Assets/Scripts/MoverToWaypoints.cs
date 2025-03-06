using UnityEngine;

public class MoverToWaypoints : MonoBehaviour
{
    [SerializeField] private Way _way;
    [SerializeField] private float _speedMovement;
    [SerializeField] private bool _isLookingToWaypoint = false;

    private Vector3 _currentWaypointPosition;

    private void Start()
    {
        if (_way == null)
            gameObject.SetActive(false);

        Initialize();
    }

    private void Update()
    {
        Move();
    }

    private void Initialize()
    {
        _currentWaypointPosition = _way.GetNextPosition();
    }

    private void Move()
    {
        if (transform.position == _currentWaypointPosition)
            _currentWaypointPosition = _way.GetNextPosition();

        transform.position = Vector3.MoveTowards(transform.position, _currentWaypointPosition, _speedMovement * Time.deltaTime);

        if (_isLookingToWaypoint == false)
            return;

        transform.LookAt(_currentWaypointPosition, transform.up);
    }
}