using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfiguration", menuName = "Configurations/create new scene configuration")]
public class SceneConfiguration : ScriptableObject
{
    public string SceneName;
    public GameObject[] EnemiesPrefabs;
}