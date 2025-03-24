using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    [Header("For Gizmos")]
    [SerializeField] private float _radiusGizmo = 0.1f;

    private int _currentWaypoint = -1;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        foreach (Transform waypoint in _waypoints)
            Gizmos.DrawSphere(waypoint.transform.position, _radiusGizmo);
    }

    public Vector2 GetNextPosition()
    {
        if (_waypoints.Length <= 0)
            return transform.position;

        _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        return _waypoints[_currentWaypoint].position;
    }

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        _waypoints = new Transform[transform.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = transform.GetChild(i);
    }
}