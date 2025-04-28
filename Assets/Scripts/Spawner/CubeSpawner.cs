using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    protected override Cube Create()
    {
        Cube cube = base.Create();

        cube.Destroyed += HandleRelease;

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

        Destroy(cube.gameObject);
    }
}