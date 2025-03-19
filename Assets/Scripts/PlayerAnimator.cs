using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LogParameters()
    {
        float speed = _animator.GetFloat(PlayerAnimatorData.Parameters.Speed);
        bool isGrounded = _animator.GetBool(PlayerAnimatorData.Parameters.IsGrounded);
        int stepsAmount = _animator.GetInteger(PlayerAnimatorData.Parameters.StepsAmount);

        Debug.Log($"Текущая скорость - {speed}");
        Debug.Log(isGrounded ? "Мы стоим на земле" : "Мы падаем");
        Debug.Log($"Мы делаем {stepsAmount} шагов чтобы разогнаться");
    }

    private void Setup(float speed, bool isGrounded, int stepsAmount, bool shouldAttack)
    {
        _animator.SetFloat(PlayerAnimatorData.Parameters.Speed, speed);
        _animator.SetBool(PlayerAnimatorData.Parameters.IsGrounded, isGrounded);
        _animator.SetInteger(PlayerAnimatorData.Parameters.StepsAmount, stepsAmount);

        if (shouldAttack)
            _animator.SetTrigger(PlayerAnimatorData.Parameters.Attack);
    }
}

public static class PlayerAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int StepsAmount = Animator.StringToHash(nameof(StepsAmount));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}