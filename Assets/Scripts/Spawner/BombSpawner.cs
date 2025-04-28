using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public override void Spawn()
    {
        if (_objectPool.CountActive < _maxSize)
            _objectPool.Get();
    }

    protected override Bomb Create()
    {
        Bomb bomb = base.Create();

        bomb.Exploded += HandleRelease;

        return bomb;
    }

    protected override void Get(Bomb bomb)
    {
        Vector3 position = _zone.GetRandomPosition();

        bomb.Initialize(position);

        base.Get(bomb);
    }

    protected override void Destroy(Bomb bomb)
    {
        bomb.Exploded -= HandleRelease;

        Destroy(bomb.gameObject);
    }
}