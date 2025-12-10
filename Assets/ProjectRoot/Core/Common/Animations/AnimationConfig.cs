using UnityEngine;

[CreateAssetMenu(fileName = "EntityAnimationConfig", menuName = "Configs/Animation/EntityAnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    [field: Header("Idle")]
    [field: SerializeField] public AnimationClip IdleClip { get; private set; }

    [field: Header("Movement")]
    [field: SerializeField] public AnimationClip WalkClip { get; private set; }
    [field: SerializeField] public AnimationClip RunClip { get; private set; }
    [field: SerializeField] public AnimationClip JumpClip { get; private set; }

    [field: Header("Attack")]
    [field: SerializeField] public AnimationClip AttackClip { get; private set; }

    [field: Header("Death")]
    [field: SerializeField] public AnimationClip DieClip { get; private set; }
}