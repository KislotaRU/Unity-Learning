using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _chanceToSpawn = 1.0f;

    public void Spawn(GameObject gameObject, Vector3 scale)
    {
        int minCountCubes = 2;
        int maxCountCubes = 6;
        int cubesCount = Random.Range(minCountCubes, maxCountCubes);

        for (int i = 0; i < cubesCount; i++)
            Instantiate(gameObject);
    }
}