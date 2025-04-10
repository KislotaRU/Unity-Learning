using UnityEngine;

public class BoxDetector2D : Detector2D
{
    [SerializeField] protected Vector2 _size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(Center, _size);
    }

    public override bool TryGetTarget(out Collider2D target)
    {
        Collider2D collider2D = Physics2D.OverlapBox(Center, _size, 0f, _targetLayer);

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
        Collider2D[] colliders2D = Physics2D.OverlapBoxAll(Center, _size, 0f, _targetLayer);

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