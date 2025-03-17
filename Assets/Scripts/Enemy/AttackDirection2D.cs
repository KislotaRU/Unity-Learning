using System;
using UnityEngine;

[Serializable]
public struct AttackDirection2D
{
    [Range(-1, 1)] public int DirectionX;
    [Range(-1, 1)] public int DirectionY;

    [Space]
    [Range(1, 50)] public int Range;

    public readonly Vector2 Direction => new Vector2(DirectionX, DirectionY).normalized;
}