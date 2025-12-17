using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingService : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreenPrefab;

    private LoadingScreen _currentLoadingScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //public async UniTask ShowLoading(string sceneName)
    //{
    //    _currentLoadingScreen = Instantiate(_loadingScreenPrefab);
    //    DontDestroyOnLoad(_currentLoadingScreen.gameObject);

    //    await _currentLoadingScreen.ShowAsync(sceneName);
    //}

    //public async UniTask HideLoading()
    //{
    //    if (_currentLoadingScreen != null)
    //    {
    //        await _currentLoadingScreen.HideAsync();
    //        Destroy(_currentLoadingScreen.gameObject);
    //    }
    //}
}