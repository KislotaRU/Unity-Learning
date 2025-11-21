using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract void Attack();

    public abstract void Reload();
}