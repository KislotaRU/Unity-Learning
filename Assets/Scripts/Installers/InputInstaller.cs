using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputActions>()
            .AsSingle()
            .NonLazy();
    }
}