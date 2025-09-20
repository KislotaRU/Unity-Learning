using UnityEngine;

[CreateAssetMenu(fileName = "MovingConfiguration", menuName = "State Configurations/MovingConfiguration")]
public class MovingConfiguration : ScriptableObject
{
    [Range(0, 10)] public float MoveSpeed;
    [Range(0, 10)] public float RotateSpeed;
}