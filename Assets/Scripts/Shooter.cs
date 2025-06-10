using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] SpawnerBullet _spawnerBullet;

    [Header("Time in second")]
    [SerializeField, Min(0)] private int _maxCooldown;

    private int _cooldown;

    private Coroutine _corountineCooldowning;
    private Coroutine _ñorountineShootingProcess;

    private bool IsCooldowning => _cooldown > 0;

    private void OnDisable()
    {
        StopShooting();
        StopCooldowning();
    }

    public void Shoot()
    {
        if (_corountineCooldowning != null)
            return;

        _cooldown = _maxCooldown;

        _corountineCooldowning = StartCoroutine(Cooldowning());

        _spawnerBullet.Spawn();
    }

    public void StartShooting()
    {
        if (_ñorountineShootingProcess == null)
            _ñorountineShootingProcess = StartCoroutine(ShootingProcess());
    }

    private void StopShooting()
    {
        if (_ñorountineShootingProcess == null)
            return;

        StopCoroutine(_ñorountineShootingProcess);

        _ñorountineShootingProcess = null;
    }

    private void StopCooldowning()
    {
        if (_corountineCooldowning == null)
            return;

        StopCoroutine(_corountineCooldowning);

        _corountineCooldowning = null;
    }

    private IEnumerator ShootingProcess()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (true)
        {
            if (_cooldown <= 0)
                Shoot();

            yield return delay;
        }
    }

    private IEnumerator Cooldowning()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (IsCooldowning)
        {
            _cooldown--;

            yield return delay;
        }

        _corountineCooldowning = null;
    }
}