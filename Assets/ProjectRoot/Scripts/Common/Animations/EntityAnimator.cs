using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
    [SerializeField] private AnimationConfig _animationConfiguration;

    private Animator _animator;
    private AnimatorOverrideController _overrideController;

    private void Awake()
    {
        if (_animationConfiguration == null)
            throw new ArgumentNullException(nameof(_animationConfiguration));

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _overrideController;

        _overrideController[TypeMotion.Idle.ToString()] = _animationConfiguration.IdleClip;

        _overrideController[TypeMotion.Walk.ToString()] = _animationConfiguration.WalkClip;
        _overrideController[TypeMotion.Run.ToString()] = _animationConfiguration.RunClip;
        _overrideController[TypeMotion.Jump.ToString()] = _animationConfiguration.JumpClip;

        _overrideController[TypeMotion.Attack.ToString()] = _animationConfiguration.AttackClip;
        _overrideController[TypeMotion.Die.ToString()] = _animationConfiguration.DieClip;
    }

    public void SetParametrs(float speed)
    {
        _animator.SetFloat(AnimatorData.Parameters.Speed, Mathf.Abs(speed));
    }
}

public static class AnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    }
}