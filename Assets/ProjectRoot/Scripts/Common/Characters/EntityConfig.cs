using UnityEngine;

public class EntityConfig : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float MaxValueHealth { get; private set; }
}