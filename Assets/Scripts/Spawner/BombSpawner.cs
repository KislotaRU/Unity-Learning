using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _spawnPosition;

    public void SpawnInPosition(Vector3 position)
    {
        _spawnPosition = position;

        base.Spawn();
    }

    protected override Bomb Create()
    {
        Bomb bomb = base.Create();

        bomb.Detonated += HandleRelease;

        return bomb;
    }

    protected override void Get(Bomb bomb)
    {
        base.Get(bomb);

        bomb.Initialize(_spawnPosition);
    }

    protected override void Destroy(Bomb bomb)
    {
        bomb.Detonated -= HandleRelease;

        Destroy(bomb.gameObject);
    }
}