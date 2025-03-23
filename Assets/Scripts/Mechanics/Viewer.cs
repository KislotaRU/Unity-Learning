using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private int _range;

    private Vector2 _position;
    private Vector2 _direction;

    public Vector2 TargetPosition { get; private set; }
    public bool IsDetected { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 directionRay = _direction * _range;

        Gizmos.DrawRay(_position, directionRay);
    }

    public bool TrySeeTarget(Vector2 position, Vector2 direction)
    {
        float distance = Mathf.Abs(direction.x) * _range;
        
        _position = position;
        _direction = direction;

        RaycastHit2D[] raycastHits2D = Physics2D.RaycastAll(position, direction, distance);

        foreach (RaycastHit2D raycastHit2D in raycastHits2D)
        {
            if (raycastHit2D.collider.TryGetComponent(out Player player))
            {
                TargetPosition = player.transform.position;
                IsDetected = true;

                return true;
            }
        }

        IsDetected = false;

        return false;
    }
}