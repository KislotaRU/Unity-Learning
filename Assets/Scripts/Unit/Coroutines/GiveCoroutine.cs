using System.Collections;
using UnityEngine;

public class GiveCoroutine : MonoBehaviour
{
    public IEnumerator GiveItem(Item item, Transform storage)
    {
        yield return StartCoroutine(GivingItem(item, storage));
    }

    private IEnumerator GivingItem(Item item, Transform storage)
    {
        for (int i = 0; i < storage.transform.childCount; i++)
        {
            if (storage.GetChild(i).TryGetComponent(out Item temporaryItem) == false)
                continue;

            if (temporaryItem.GetType() != item.GetType())
                continue;

            item.HandleDestroy();
        }

        yield return null;
    }
}