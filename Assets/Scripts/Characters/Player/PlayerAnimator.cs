using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetParametrs(float speed)
    {
        _animator.SetFloat(PlayerAnimatorData.Parameters.Speed, Mathf.Abs(speed));
    }
}

public static class PlayerAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    }
}