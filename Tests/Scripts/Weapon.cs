using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AudioSource _shoot;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Выстрел.");
        _shoot.Play();
    }
}