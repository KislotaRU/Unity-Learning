using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private int _range;
    [SerializeField] private float _delayAttack;

    private bool _isRecharged = true;

    private Vector2 _position;
    private Vector2 _direction;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 directionRay = _direction * _range;

        Gizmos.DrawRay(_position, directionRay);
    }

    private IEnumerator RechargingAttack()
    {
        yield return new WaitForSeconds(_delayAttack);

        _isRecharged = true;
    }

    public bool TryAttack(Vector2 position, Vector2 direction, out Health targetHealth)
    {
        targetHealth = null;

        if (_isRecharged == false)
            return false;

        float distance = Mathf.Abs(direction.x) * _range;

        _position = position;
        _direction = direction;
        
        _isRecharged = false;

        StartCoroutine(RechargingAttack());

        RaycastHit2D[] raycastHits2D = Physics2D.RaycastAll(position, direction, distance);

        foreach (RaycastHit2D raycastHit2D in raycastHits2D)
        {
            Debug.Log($"Обнаружен объект: {raycastHit2D.collider.name}");

            if (raycastHit2D.collider.TryGetComponent(out Health health))
            {
                targetHealth = health;

                return true;
            }
        }

        return false;
    }
}