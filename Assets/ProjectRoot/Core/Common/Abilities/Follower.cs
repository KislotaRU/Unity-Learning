using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public Vector3 Direction
    {
        get
        {
            if (_target == null)
                return Vector3.zero;

            return (_target.position - transform.position).normalized;
        }
    }
}