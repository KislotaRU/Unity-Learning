using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private readonly List<ITimerService> _timerServices = new();

    private void Awake()
    {
        _timerServices.Add(new TimerService<IWeapon>());
    }

    private void Update()
    {
        HandleTimerService();
    }

    public ITimerService<T> GetTimerService<T>() where T : class
    {
        return _timerServices.OfType<ITimerService<T>>().FirstOrDefault();
    }

    private void HandleTimerService()
    {
        float deltaTime = Time.deltaTime;

        foreach (var timerService in _timerServices)
            timerService.Tick(deltaTime);
    }
}