using System;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; private set; }

    public Vector3 Direction
    {
        get
        {
            if (Target == null)
                return Vector3.zero;

            return (Target.position - transform.position).normalized;
        }
    }

    public Vector3 Direction2D => new(Direction.x, 0f, Direction.y);

    public void SetTarget(Transform target)
    {
        Target = target != null ? target : throw new ArgumentNullException(nameof(target));
    }
}