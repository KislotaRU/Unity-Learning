using System.Collections;
using UnityEngine;

public class MoveCoroutine : MonoBehaviour
{
    [SerializeField] private MovingConfiguration _configuration;

    public IEnumerator MoveTo(Vector3 targetPosition)
    {
        yield return StartCoroutine(Moving(targetPosition));
    }

    private IEnumerator Moving(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        while (direction.sqrMagnitude > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _configuration.MoveSpeed * Time.deltaTime);
            direction = targetPosition - transform.position;

            yield return null;
        }
    }
}