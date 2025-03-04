using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingBullets : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _pointToShoot;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _bulletSpeed;

    private readonly int _poolCapacity = 5;
    private readonly int _poolMaxSize = 5;

    private ObjectPool<Bullet> _bulletPool;

    private void Start()
    {
        Initialize();

        StartCoroutine(ShootingWorker());
    }
    
    private IEnumerator ShootingWorker()
    {
        WaitForSeconds delay = new WaitForSeconds(_shootingDelay);
        bool isShooting = gameObject.activeSelf;

        while (isShooting)
        {
            if (_bulletPool.CountActive < _poolMaxSize)
                _bulletPool.Get();

            yield return delay;
        }
    }

    private void Initialize()
    {
        _bulletPool = new ObjectPool<Bullet>(createFunc: CreateBullet,
                                             actionOnGet: (bullet) => GetBullet(bullet),
                                             actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
                                             actionOnDestroy: (bullet) => DestroyBullet(bullet),
                                             collectionCheck: true,
                                             defaultCapacity: _poolCapacity,
                                             maxSize: _poolMaxSize);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.ElapsedTimeDeactivation += ReleaseBullet;

        return bullet;
    }

    private void GetBullet(Bullet bullet)
    {
        Vector3 direction = (_pointToShoot.position - transform.position).normalized;

        bullet.Initialize(_pointToShoot.position, direction, _bulletSpeed);
        bullet.gameObject.SetActive(true);
    }

    private void ReleaseBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }

    private void DestroyBullet(Bullet bullet)
    {
        bullet.ElapsedTimeDeactivation -= ReleaseBullet;
        Destroy(bullet.gameObject);
    }
}