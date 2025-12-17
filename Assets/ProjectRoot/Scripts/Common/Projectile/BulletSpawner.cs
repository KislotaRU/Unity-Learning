public class BulletSpawner : Spawner<Projectile>
{
    public override Projectile Spawn()
    {
        Projectile bullet = base.Spawn();

        bullet.Destroyed += HandleRelease;

        return bullet;
    }

    protected override void Release(Projectile bullet)
    {
        bullet.Destroyed -= HandleRelease;

        base.Release(bullet);
    }
}