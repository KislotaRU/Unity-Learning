using System.Collections;
using UnityEngine;

public class RotationCoroutine : MonoBehaviour
{
    [SerializeField] private MovingConfiguration _configuration;

    public IEnumerator RotateTo(Vector3 targetPosition)
    {
        yield return StartCoroutine(Rotating(targetPosition));
    }

    private IEnumerator Rotating(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (transform.rotation != targetRotation)
        {
            targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _configuration.RotateSpeed * Time.deltaTime);

            direction = targetPosition - transform.position;

            yield return null;
        }
    }
}