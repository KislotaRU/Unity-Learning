using UnityEngine;

public class ProjectileConfiguration : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float LifeTime { get; private set; }
}