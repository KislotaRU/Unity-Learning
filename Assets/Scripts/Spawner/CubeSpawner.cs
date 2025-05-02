using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] protected SpawnZone _zone;
    [Space]
    [SerializeField] protected BombSpawner _bombSpawner;

    protected override Cube Create()
    {
        Cube cube = base.Create();

        cube.Destroyed += HandleRelease;

        if ( _bombSpawner != null )
            cube.BombSpawning += _bombSpawner.SpawnInPosition;

        return cube;
    }

    protected override void Get(Cube cube)
    {
        Vector3 position = _zone.GetRandomPosition();

        cube.Initialize(position);

        base.Get(cube);
    }

    protected override void Destroy(Cube cube)
    {
        cube.Destroyed -= HandleRelease;

        if (_bombSpawner != null)
            cube.BombSpawning -= _bombSpawner.SpawnInPosition;

        Destroy(cube.gameObject);
    }
}