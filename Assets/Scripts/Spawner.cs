using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Min(1)] private int _minCount = 2;
    [SerializeField, Min(2)] private int _maxCount = 6;
    [Space]
    [SerializeField, Min(1)] private int _reductionFactorChanceToSpawn = 2;
    [SerializeField, Min(1)] private int _reductionFactorScale = 2;

    private void OnValidate()
    {
        if (_minCount >= _maxCount)
            _minCount = _maxCount - 1;
    }

    public void Spawn(Cube cube)
    {
        int objectsCount = Random.Range(_minCount, _maxCount);

        float newCubeChanceToSpawn = cube.ChanceToSpawn / _reductionFactorChanceToSpawn;
        Vector3 newCubeScale = cube.transform.localScale / _reductionFactorScale;

        for (int i = 0; i < objectsCount; i++)
        {
            var newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);

            newCube.Initialize(newCubeScale, newCubeChanceToSpawn);
        }
    }
}