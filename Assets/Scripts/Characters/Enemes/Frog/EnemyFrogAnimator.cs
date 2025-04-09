using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyFrogAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Setup(float speed)
    {
        _animator.SetFloat(PlayerAnimatorData.Parameters.Speed, Mathf.Abs(speed));
    }
}

public static class EnemyFrogAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    }
}