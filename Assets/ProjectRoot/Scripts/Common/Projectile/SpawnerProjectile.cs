using System;
using UnityEngine;

public class SpawnerProjectile : Spawner<Projectile>
{
    [Header("Settings SpawnerProjectile")]
    [SerializeField] protected Transform _container;

    public override Projectile Spawn()
    {
        return base.Spawn();
    }

    protected override void Get(Projectile @object)
    {
        @object.Destroyed += OnRelease;

        @object.transform.parent = null;

        base.Get(@object);
    }

    protected override void Release(Projectile @object)
    {
        @object.Destroyed -= OnRelease;

        base.Release(@object);

        @object.transform.parent = _container;
    }
}