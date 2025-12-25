using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _config;
    [SerializeField] private AmmoController _ammoController;
    [SerializeField] private LoadingScreen _loadingScreen;

    public override void InstallBindings()
    {
        InputInstall();
        TimerInstall();
        AmmoInstall();

        Container.Bind<SceneLoader>()
            .AsSingle()
            .NonLazy();

        Container.Bind<GameConfig>()
            .FromInstance(_config)
            .AsSingle();

        Container.Bind<LoadingScreen>()
            .FromComponentInNewPrefab(_loadingScreen)
            .AsSingle();
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