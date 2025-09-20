using UnityEngine;

[CreateAssetMenu(fileName = "CollectingConfiguration", menuName = "State Configurations/CollectingConfiguration")]
public class CollectingConfiguration : ScriptableObject
{
    [Range(0, 10)] public float CaptureRadius;

    public LayerMask TargetMask;
}