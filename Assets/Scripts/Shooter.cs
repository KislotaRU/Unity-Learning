using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] SpawnerBullet _spawnerBullet;

    [Header("Time in second")]
    [SerializeField, Min(0)] private int _maxCooldown;

    private int _cooldown = 0;

    private bool IsCooldowning => _cooldown > 0;

    private Coroutine _currentCorountine;

    private void OnDisable()
    {
        _currentCorountine = null;
    }

    private IEnumerator Cooldowning()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (IsCooldowning)
        {
            _cooldown--;

            yield return delay;
        }

        _currentCorountine = null;
    }

    public void Shoot()
    {
        if (_currentCorountine != null)
            return;

        _cooldown = _maxCooldown;
        _currentCorountine = StartCoroutine(Cooldowning());

        _spawnerBullet.Spawn();
    }
}