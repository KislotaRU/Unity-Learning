using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfigurator", menuName = "Entity Configurator/EntityConfigurator")]
public class EntityConfigurator : ScriptableObject
{
    [Min(0f)] public float MaxValueHealth;
}