using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KatanaAnimator : MonoBehaviour
{
    private const string AttackKatana = nameof(AttackKatana);

    [SerializeField] private Animator _animator;

    public void Setup(bool isAttacking)
    {
        _animator.SetBool(KatanaAnimatorData.Parameters.IsAttacking, isAttacking);
    }

    public void Play()
    {
        _animator.Play(AttackKatana);
    }
}

public static class KatanaAnimatorData
{
    public static class Parameters
    {
        public static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    }
}