public class BulletSpawner : Spawner<Bullet>
{
    public override Bullet Spawn()
    {
        Bullet bullet = base.Spawn();

        bullet.Destroyed += HandleRelease;

        return bullet;
    }

    protected override void Release(Bullet bullet)
    {
        bullet.Destroyed -= HandleRelease;

        base.Release(bullet);
    }
}