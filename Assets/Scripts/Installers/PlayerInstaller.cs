using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;

    [SerializeField] private Transform _parent;

    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Healer _healer;

    public override void InstallBindings()
    {
        BindComponents();
        BindController();
        BindPlayer();
    }

    private void BindComponents()
    {
        Container.Bind<IMover>()
            .FromComponentInNewPrefab(_mover)
            .AsSingle()
            .WhenInjectedInto<MovementInputHandler>();

        Container.Bind<IRotator>()
            .FromComponentInNewPrefab(_rotator)
            .AsSingle()
            .WhenInjectedInto<MovementInputHandler>();

        Container.Bind<IAttacker>()
            .FromComponentInNewPrefab(_attacker)
            .AsSingle()
            .WhenInjectedInto<CombatInputHandler>();

        Container.Bind<IHealer>()
            .FromComponentInNewPrefab(_healer)
            .AsSingle()
            .WhenInjectedInto<CombatInputHandler>();
    }

    private void BindController()
    {
        Container.Bind<MovementInputHandler>()
            .AsSingle()
            .NonLazy();
    }

    private void BindPlayer()
    {
        Container.Bind<Player>()
            .FromComponentInNewPrefab(_playerPrefab)
            .AsSingle()
            .NonLazy();
    }
}