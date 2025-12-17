using UnityEngine;
using Zenject;

public class BootstrapperSceneInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LoadingScreen _loadingScreenPrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameConfig);

        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        //Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle().NonLazy();
    }
}