using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
    private const string IdleMotion = "Idle";

    private const string WalkMotion = "Walk";
    private const string RunMotion = "Run";
    private const string JumpMotion = "Jump";

    private const string AttackMotion = "Attack";
    private const string DieMotion = "Die";

    [SerializeField] private AnimationConfiguration _animationConfiguration;

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

        _overrideController[IdleMotion] = _animationConfiguration.IdleClip;

        _overrideController[WalkMotion] = _animationConfiguration.WalkClip;
        _overrideController[RunMotion] = _animationConfiguration.RunClip;
        _overrideController[JumpMotion] = _animationConfiguration.JumpClip;

        _overrideController[AttackMotion] = _animationConfiguration.AttackClip;
        _overrideController[DieMotion] = _animationConfiguration.DieClip;
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