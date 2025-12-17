using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private AmmoController _ammoController;

    public override void InstallBindings()
    {
        AmmoInstall();
    }

    private void AmmoInstall()
    {
        Container.Bind<AmmoController>()
            .FromComponentInNewPrefab(_ammoController)
            .AsSingle();
    }
}