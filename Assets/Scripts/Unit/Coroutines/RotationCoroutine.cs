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

        float dotThreshold = 1f;

        while (Vector3.Dot(transform.forward, direction.normalized) < dotThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _configuration.RotateSpeed * Time.deltaTime);
            direction = targetPosition - transform.position;

            if (direction != Vector3.zero)
                targetRotation = Quaternion.LookRotation(direction);

            yield return null;
        }

        transform.rotation = targetRotation;
    }
}