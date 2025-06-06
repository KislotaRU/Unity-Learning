using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    [SerializeField] private Transform _container;
    [Space]
    [SerializeField] private Transform _direction;

    private IShooter _shooter;

    private void Start()
    {
        _shooter = transform.parent.gameObject.GetComponent<IShooter>();
    }

    protected override Bullet Create()
    {
        Bullet bullet = base.Create();

        bullet.transform.parent = _container;

        bullet.Destroyed += HandleRelease;

        return bullet;
    }

    protected override void Get(Bullet bullet)
    {
        bullet.Initialize(_direction.transform.position, _direction.transform.rotation, _shooter);

        base.Get(bullet);
    }

    protected override void Destroy(Bullet bullet)
    {
        bullet.Destroyed -= HandleRelease;

        Destroy(bullet.gameObject);
    }
}