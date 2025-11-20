using System;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    private BulletConfiguration _configuration;

    private ITimerService<Bullet> _timerService;
    private IMover _mover;

    [Inject]
    private void Construct(BulletConfiguration configuration, ITimerService<Bullet> timerService, IMover mover)
    {
        _configuration = configuration;
        _timerService = timerService;
        _mover = mover;
    }

    public event Action<IHealth> Hit;
    public event Action<Bullet> Destroyed;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        if (_timerService != null)
            throw new ArgumentNullException(nameof(_timerService));

        if (_mover != null)
            throw new ArgumentNullException(nameof(_mover));
    }

    private void Update()
    {
        _mover.HandleMove(Direction);
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