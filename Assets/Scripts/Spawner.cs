using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minCount = 2;
    [SerializeField, Min(2)] private int _maxCount = 6;

    private void OnValidate()
    {
        if (_minCount >= _maxCount)
            _minCount = _maxCount - 1;
    }

    public List<Cube> Spawn(Cube cube, Vector3 position)
    {
        List<Cube> cubes = new List<Cube>();
        int objectsCount = Random.Range(_minCount, _maxCount);

        for (int i = 0; i < objectsCount; i++)
        {
            var spawnedCube = Instantiate(cube, position, Quaternion.identity);
            spawnedCube.Initialize();
            cubes.Add(spawnedCube);
        }

        return cubes;
    }
}