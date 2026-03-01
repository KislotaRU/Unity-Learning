using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 51)]
public class GameConfig : ScriptableObject
{
    [field: Header("Scenes")]
    [field: SerializeField] public Scenes FirstScene { get; private set; }
}