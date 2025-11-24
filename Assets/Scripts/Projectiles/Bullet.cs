using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ITimerService<Bullet> _timerService;

    public event Action<IHealth> Hit;
    public event Action<Bullet> Destroyed;

    public float ProjectileVelocity { get; private set; }

    private void Awake()
    {
        _timerService = new TimerService<Bullet>();
    }

    private void Update()
    {
        transform.position += ProjectileVelocity * Time.deltaTime * transform.forward;

        _timerService.Tick(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet _))
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