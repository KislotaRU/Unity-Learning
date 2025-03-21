using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Setup(float speed, bool isGrounded)
    {
        _animator.SetFloat(PlayerAnimatorData.Parameters.Speed, Mathf.Abs(speed));
        _animator.SetBool(PlayerAnimatorData.Parameters.IsGrounded, isGrounded);
    }
}

public static class PlayerAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}