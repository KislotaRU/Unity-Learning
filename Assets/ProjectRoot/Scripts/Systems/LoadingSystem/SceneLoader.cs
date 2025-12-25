using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly LoadingScreen _loadingScreen;

    public SceneLoader(LoadingScreen loadingScreen)
    {
        _loadingScreen = loadingScreen != null ? loadingScreen : throw new ArgumentNullException(nameof(loadingScreen));
    }

    public async UniTaskVoid LoadSceneWithLoadingScreenAsync(string sceneName)
    {
        _loadingScreen.SetName(sceneName);

        await _loadingScreen.Show();

        IProgress<float> progress = Progress.Create<float>(value => _loadingScreen.SetProgress(value));

        _loadingScreen.SetMessage("Loading Scene...");
        progress.Report(0.12f);
        await UniTask.Delay(1000);
        await LoadSceneAsync(sceneName);

        _loadingScreen.SetMessage($"Completion...");
        progress.Report(1.0f);
        await UniTask.Delay(1000);

        await _loadingScreen.Hide();
    }

    public async UniTask LoadSceneAsync(string sceneName)
    {
        await Addressables.LoadSceneAsync(sceneName);
    }

    public async UniTask ReloadSceneAsync()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        await LoadSceneAsync(currentScene);
    }
}