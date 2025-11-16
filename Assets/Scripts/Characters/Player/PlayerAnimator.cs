using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private MovementInputHandler _playerController;
    private Animator _animator;

    [Inject]
    private void Construct(MovementInputHandler playerController)
    {
        _playerController = playerController;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateParametrs()
    {
        HandleMove();
    }

    public void HandleMove()
    {
        _animator.SetFloat(PlayerAnimatorData.Parameters.Speed, Mathf.Abs(_playerController.Mover.CurrentSpeed));
    }
}

public static class PlayerAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    }
}