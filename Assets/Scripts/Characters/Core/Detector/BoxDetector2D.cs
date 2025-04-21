using UnityEngine;

public class BoxDetector2D : Detector2D
{
    [SerializeField] protected Vector2 _size;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        Gizmos.DrawWireCube(Center, _size);
    }

    public override bool TryGetTarget(out Collider2D target, Collider2D collider2D = null)
    {
        collider2D = Physics2D.OverlapBox(Center, _size, 0f, _targetLayer);

        return base.TryGetTarget(out target, collider2D);
    }

    public override bool TryGetTargets(out Collider2D[] targets, Collider2D[] colliders2D = null)
    {
        colliders2D = Physics2D.OverlapBoxAll(Center, _size, 0f, _targetLayer);

        return base.TryGetTargets(out targets, colliders2D);
    }
}