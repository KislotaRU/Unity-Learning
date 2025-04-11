using UnityEngine;

public class CircleDetector2D : Detector2D
{
    [SerializeField] protected float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        Gizmos.DrawWireSphere(Center, _radius);
    }

    public override bool TryGetTarget(out Collider2D target)
    {
        Collider2D collider2D = Physics2D.OverlapCircle(Center, _radius, _targetLayer);

        target = collider2D;
        IsDetected = collider2D != null;

        return IsDetected;
    }

    public override bool TryGetTargets(out Collider2D[] targets)
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(Center, _radius, _targetLayer);

        targets = colliders2D;
        IsDetected = colliders2D != null;

        return IsDetected;
    }
}