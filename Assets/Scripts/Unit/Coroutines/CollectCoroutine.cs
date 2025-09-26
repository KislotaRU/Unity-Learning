using System.Collections;
using UnityEngine;

public class CollectCoroutine : MonoBehaviour
{
    [SerializeField] private CollectingConfiguration _configuration;

    public IEnumerator Collect(Item item, Transform storage)
    {
        yield return StartCoroutine(Collecting(item, storage));
    }

    private IEnumerator Collecting(Item item, Transform storage)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _configuration.CaptureRadius, _configuration.TargetMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Item temporaryItem) == false)
                continue;

            if (temporaryItem.GetType() != item.GetType())
                continue;
            
            item.transform.SetParent(storage);
            item.transform.position = storage.position;

            item.HandleCollect();
            break;
        }

        yield return null;
    }
}