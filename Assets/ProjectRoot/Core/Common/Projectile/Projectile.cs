using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ITimerService<Projectile> _timerService;

    public event Action<IHealth> Hit;
    public event Action<Projectile> Destroyed;

    public float ProjectileVelocity { get; private set; }

    private void Awake()
    {
        _timerService = new TimerService<Projectile>();
    }

    private void Update()
    {
        transform.position += ProjectileVelocity * Time.deltaTime * transform.forward;

        _timerService.Tick(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile _))
            return;

        if (other.TryGetComponent(out IHealth health))
            Hit?.Invoke(health);

        Destroyed?.Invoke(this);
        _timerService.StopAll(this);
    }

    private void OnDestroy()
    {
        Hit = null;
        Destroyed = null;
    }

    public void Initialize(Vector3 position, Quaternion rotation, float projectileVelocity, float projectileLifeTime)
    {
        transform.position = position;
        transform.rotation = rotation;

        ProjectileVelocity = projectileVelocity;

        _timerService.CreateTimer(this, projectileLifeTime, () =>
        {
            Destroyed?.Invoke(this);
        });
    }
}