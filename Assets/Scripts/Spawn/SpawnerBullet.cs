using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    [SerializeField] private Transform _position;

    private IReceiverScore _shooter;

    protected override void Awake()
    {
        base.Awake();

        _shooter = transform.parent.GetComponent<IReceiverScore>();
    }

    protected override Bullet Create()
    {
        Bullet bullet = base.Create();

        bullet.transform.parent = Containers.BulletContainer;

        bullet.Destroyed += HandleRelease;

        return bullet;
    }

    protected override void Get(Bullet bullet)
    {
        bullet.Initialize(_position.transform.position, _position.transform.rotation, _shooter);

        base.Get(bullet);
    }

    protected override void Destroy(Bullet bullet)
    {
        bullet.Destroyed -= HandleRelease;

        Destroy(bullet.gameObject);
    }
}