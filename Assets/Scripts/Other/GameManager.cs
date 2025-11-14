using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static ITimerService<IWeapon> _weaponTimerService;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _weaponTimerService = new TimerService<IWeapon>();
    }

    private void Update()
    {
        HandleTimerService();
    }

    public static ITimerService<T> GetTimerService<T>() where T : ITimerService<T>
    {
        switch (typeof(T))
        {
            case ITimerService<IWeapon>: return (ITimerService<T>)_weaponTimerService;
        }

        throw new InvalidOperationException();
    }

    private void HandleTimerService()
    {
        float deltaTime = Time.deltaTime;

        _weaponTimerService.Tick(deltaTime);
    }
}