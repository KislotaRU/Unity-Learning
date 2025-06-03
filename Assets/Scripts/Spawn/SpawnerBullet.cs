using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    [Space]
    [SerializeField] private Transform _container;
    [Space]
    [SerializeField] private DirectionBullet _directionBullet;

    protected override Bullet Create()
    {
        Bullet bullet = base.Create();

        bullet.transform.parent = _container;

        //bullet.Destroyed += HandleRelease;

        //if (_bombSpawner != null)
        //    bullet.BombSpawning += _bombSpawner.SpawnInPosition;

        return bullet;
    }

    protected override void Get(Bullet bullet)
    {
        bullet.Initialize(_directionBullet.transform.position, _directionBullet.transform.right);

        base.Get(bullet);
    }

    protected override void Destroy(Bullet bullet)
    {
        //bullet.Destroyed -= HandleRelease;

        //if (_bombSpawner != null)
        //    bullet.BombSpawning -= _bombSpawner.SpawnInPosition;

        Destroy(bullet.gameObject);
    }
}