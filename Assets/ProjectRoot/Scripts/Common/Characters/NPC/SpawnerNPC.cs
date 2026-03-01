using UnityEngine;

public class SpawnerNPC : Spawner<NPC>
{
    [Header("Settings SpawnerNPC")]
    [SerializeField] protected Transform _container;
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private Transform _target;

    [ContextMenu("Spawn")]
    public override NPC Spawn()
    {
        return base.Spawn();
    }

    protected override void Get(NPC @object)
    {
        @object.transform.position = _spawnZone.GetPosition();
        @object.SetTarget(_target);

        @object.Died += OnRelease;

        @object.transform.parent = null;

        base.Get(@object);
    }

    protected override void Release(NPC @object)
    {
        @object.Died -= OnRelease;

        base.Release(@object);

        @object.transform.parent = _container;
    }
}