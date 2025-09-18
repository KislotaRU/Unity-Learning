using UnityEngine;

[CreateAssetMenu(fileName = "MovingStateConfiguration", menuName = "State Configurations/MovingStateConfiguration")]
public class MovingStateConfiguration : ScriptableObject
{
    [Range(0, 10)] public float MoveSpeed;
}