using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypoint = -1;

    public Vector3 GetNextPosition()
    {
        if (_waypoints.Length > 0)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

            return _waypoints[_currentWaypoint].position;
        }

        return transform.position;
    }

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        _waypoints = new Transform[transform.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = transform.GetChild(i);
    }
}