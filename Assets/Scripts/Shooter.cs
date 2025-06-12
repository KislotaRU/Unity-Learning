using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] SpawnerBullet _spawnerBullet;

    [Header("Time in second")]
    [SerializeField, Min(0)] private int _maxCooldown;

    private int _cooldown;

    private Coroutine _corountineCooldowningProcess;
    private Coroutine _�orountineShootingProcess;

    private bool IsCooldowning => _cooldown > 0;

    private void OnDisable()
    {
        StopCooldowning();
        StopShooting();
    }

    public void Shoot()
    {
        if (_corountineCooldowningProcess != null)
            return;

        _cooldown = _maxCooldown;

        _corountineCooldowningProcess = StartCoroutine(CooldowningProcess());

        _spawnerBullet.Spawn();
    }

    public void StartShooting()
    {
        if (_�orountineShootingProcess == null)
            _�orountineShootingProcess = StartCoroutine(ShootingProcess());
    }

    private void StopShooting()
    {
        if (_�orountineShootingProcess == null)
            return;

        StopCoroutine(_�orountineShootingProcess);

        _�orountineShootingProcess = null;
    }

    private void StopCooldowning()
    {
        if (_corountineCooldowningProcess == null)
            return;

        StopCoroutine(_corountineCooldowningProcess);

        _corountineCooldowningProcess = null;
        _cooldown = 0;
    }

    private IEnumerator ShootingProcess()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (enabled)
        {
            if (_cooldown <= 0)
                Shoot();

            yield return delay;
        }
    }

    private IEnumerator CooldowningProcess()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (IsCooldowning)
        {
            _cooldown--;

            yield return delay;
        }

        _corountineCooldowningProcess = null;
    }
}