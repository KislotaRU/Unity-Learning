using UnityEngine;

public abstract class Detector2D : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected float _offset;
    [Space]
    [SerializeField] protected Color _color = Color.red;
    [SerializeField] protected bool _isFixedPosition;

    public bool IsDetected { get; protected set; }

    protected Vector2 Position => (Vector2)transform.position + (Vector2)transform.right * _offset;
    protected Vector2 FixedPosition { get; private set; }
    protected Vector2 Center => _isFixedPosition ? FixedPosition : Position;

    private void OnValidate()
    {
        FixedPosition = (Vector2)transform.position + (Vector2)transform.right * _offset;
    }

    public virtual bool TryGetTarget(out Collider2D target, Collider2D collider2D = null)
    {
        target = collider2D;

        return IsDetected = collider2D != null;
    }

    public virtual bool TryGetTargets(out Collider2D[] targets, Collider2D[] colliders2D = null)
    {
        targets = colliders2D;

        return IsDetected = colliders2D != null;
    }
}