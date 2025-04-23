using UnityEngine;

public class BoxDetector2D : Detector2D
{
    [SerializeField] protected Vector2 _size;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        Gizmos.DrawWireCube(Center, _size);
    }

    public override bool TryGetTarget(out Collider2D target)
    {
        target = Physics2D.OverlapBox(Center, _size, 0f, _targetLayer);

        return IsDetected = target != null;
    }

    public override bool TryGetTargets(out Collider2D[] targets)
    {
        targets = Physics2D.OverlapBoxAll(Center, _size, 0f, _targetLayer);

        return IsDetected = targets != null;
    }
}