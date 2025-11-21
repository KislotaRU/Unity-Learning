using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfiguration _configuration;
    [SerializeField] private Mover _mover;

    private ITimerService<Bullet> _timerService;

    public event Action<IHealth> Hit;
    public event Action<Bullet> Destroyed;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        _timerService = new TimerService<Bullet>();
    }

    private void Update()
    {
        _mover.HandleMove(Direction);
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

    public void Initialize(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        Direction = direction;

        _timerService.CreateTimer(this, _configuration.LifeTime, () =>
        {
            Destroyed?.Invoke(this);
        });
    }
}