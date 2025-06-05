using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    [SerializeField] private Transform _container;
    [Space]
    [SerializeField] private DirectionBullet _directionBullet;

    protected override Bullet Create()
    {
        Bullet bullet = base.Create();

        bullet.transform.parent = _container;

        return bullet;
    }

    protected override void Get(Bullet bullet)
    {
        bullet.Initialize(_directionBullet.transform.position, _directionBullet.Direction);

        base.Get(bullet);
    }
}