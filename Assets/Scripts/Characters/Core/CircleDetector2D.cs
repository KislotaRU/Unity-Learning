using UnityEngine;

public class CircleDetector2D : Detector2D
{
    [SerializeField] protected float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(Center, _radius);
    }

    public override bool TryGetTarget(out Collider2D target)
    {
        Collider2D collider2D = Physics2D.OverlapCircle(Center, _radius, _targetLayer);

        if (collider2D != null)
        {
            target = collider2D;
            IsDetected = true;

            return true;
        }

        IsDetected = false;
        target = null;

        return false;
    }

    public override bool TryGetTargets(out Collider2D[] targets)
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(Center, _radius, _targetLayer);

        if (colliders2D != null)
        {
            targets = colliders2D;
            IsDetected = true;

            return true;
        }

        IsDetected = false;
        targets = null;

        return false;
    }
}