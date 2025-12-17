using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject _assetReference;

    private AsyncOperationHandle<GameObject> _handle;

    private async void Start()
    {
        _handle = Addressables.LoadAssetAsync<GameObject>(_assetReference);

        GameObject prefab = await _handle.Task;

        if (prefab != null)
            Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if (_handle.IsValid())
            Addressables.Release(_handle);
    }
}