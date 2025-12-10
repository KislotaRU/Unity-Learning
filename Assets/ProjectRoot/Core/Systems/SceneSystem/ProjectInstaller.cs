using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AmmoController _ammoController;

    public override void InstallBindings()
    {
        InputInstall();
        AmmoInstall();
        TimerInstall();
    }

    private void InputInstall()
    {
        Container.Bind<InputActions>()
            .AsSingle()
            .NonLazy();
    }

    private void AmmoInstall()
    {
        Container.Bind<AmmoController>()
            .FromComponentInNewPrefab(_ammoController)
            .AsSingle();
    }

    private void TimerInstall()
    {
        Container.Bind<ITimerService<IWeapon>>()
            .To<TimerService<IWeapon>>()
            .AsSingle()
            .NonLazy();
    }
}