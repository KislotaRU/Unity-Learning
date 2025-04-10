using UnityEngine;

public abstract class Detector2D : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected float _offset;
    public bool IsDetected { get; protected set; }

    protected Vector2 Center => ((Vector2)transform.position + ((Vector2)transform.right * _offset));

    public abstract bool TryGetTarget(out Collider2D target);
    public abstract bool TryGetTargets(out Collider2D[] targets);
}