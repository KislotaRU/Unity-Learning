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

    public abstract bool TryGetTarget(out Collider2D target);

    public abstract bool TryGetTargets(out Collider2D[] targets);
}