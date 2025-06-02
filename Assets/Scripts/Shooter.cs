using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] SpawnerBullet _spawnerBullet;

    [Header("Time in second")]
    [SerializeField, Min(0)] private int _maxCooldown;

    private int _cooldown = 0;

    private bool IsCooldowning => _cooldown > 0;

    private IEnumerator Cooldowning()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (IsCooldowning)
        {
            _cooldown--;

            yield return delay;
        }
    }

    public void Shoot()
    {
        if (IsCooldowning)
            return;

        _cooldown = _maxCooldown;

        _spawnerBullet.Spawn();


    }
}