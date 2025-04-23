using UnityEngine;

public class VampirismAnimator : MonoBehaviour
{
    private const string AttackVampirism = nameof(AttackVampirism);

    [SerializeField] private Animator _animator;

    public void Setup(bool isAttacking)
    {
        _animator.SetBool(VampirismAnimatorData.Parameters.IsAttacking, isAttacking);
    }
}

public static class VampirismAnimatorData
{
    public static class Parameters
    {
        public static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    }
}