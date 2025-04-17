using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KatanaAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Setup(bool isAttack)
    {
        _animator.SetBool(KatanaAnimatorData.Parameters.IsAttack, isAttack);
    }
}

public static class KatanaAnimatorData
{
    public static class Parameters
    {
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    }
}