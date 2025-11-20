using UnityEngine;

public class EntityConfiguration : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float MaxValueHealth { get; private set; }
}