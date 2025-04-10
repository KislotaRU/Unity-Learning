using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] private Detector2D _jumpZone;

    public bool IsGrounded => _jumpZone.TryGetTarget(out Collider2D _);
}