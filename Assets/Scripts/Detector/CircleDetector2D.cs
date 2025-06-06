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
        target = Physics2D.OverlapCircle(Center, _radius, _targetLayer);

        return IsDetected = target != null;
    }

    public override bool TryGetTargets(out Collider2D[] targets)
    {
        targets = Physics2D.OverlapCircleAll(Center, _radius, _targetLayer);

        return IsDetected = targets != null;
    }
}