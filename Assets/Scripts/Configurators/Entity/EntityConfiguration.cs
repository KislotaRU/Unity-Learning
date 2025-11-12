using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfiguration", menuName = "Configurators/Entity/EntityConfiguration")]
public class EntityConfiguration : ScriptableObject
{
    [Min(0f)] public float MaxValueHealth;
}