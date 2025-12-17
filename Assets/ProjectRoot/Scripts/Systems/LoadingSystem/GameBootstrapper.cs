using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LoadingScreen _loadingScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_loadingScreen);
    }

    private void Start()
    {
        InitializeApplicationAsync().Forget();
    }

    private async UniTaskVoid InitializeApplicationAsync()
    {
        _loadingScreen.Show();

        _loadingScreen.UpdateMessage("Initializing...");
        _loadingScreen.UpdateProgress(0.1f);
        await InitializeCoreServicesAsync();

        _loadingScreen.UpdateMessage("Loading Configurations...");
        _loadingScreen.UpdateProgress(0.3f);
        await LoadConfigurationsAsync();

        _loadingScreen.UpdateMessage("Loading Critical Assets...");
        _loadingScreen.UpdateProgress(0.5f);
        await PreloadCriticalAssetsAsync();

        _loadingScreen.UpdateMessage("Starting game...");
        _loadingScreen.UpdateProgress(0.8f);
        await StartGameAsync();

        _loadingScreen.UpdateMessage($"Completion...");
        _loadingScreen.UpdateProgress(1f);
        await UniTask.Delay(1000);

        await _loadingScreen.Hide();
    }

    private async UniTask InitializeCoreServicesAsync()
    {
        await InitializeAudioSystemAsync();
        await InitializeInputSystemAsync();
        await InitializeSaveSystemAsync();
    }

    private async UniTask InitializeAudioSystemAsync()
    {
        // Загрузка аудио системы
        await UniTask.Delay(1000);
    }

    private async UniTask InitializeInputSystemAsync()
    {
        // Загрузка системы ввода
        await UniTask.Delay(1000);
    }

    private async UniTask InitializeSaveSystemAsync()
    {
        // Загрузка системы сохранения
        await UniTask.Delay(1000);
    }

    private async UniTask LoadConfigurationsAsync()
    {
        // Загрузка конфигов из Resources, Addressables или Bundle
        await UniTask.Delay(1000);
    }

    private async UniTask PreloadCriticalAssetsAsync()
    {
        // Загрузка важных ресурсов из Resources, Addressables или Bundle
        await UniTask.Delay(1000);
    }

    private async UniTask StartGameAsync()
    {
        await LoadSceneAsync(_gameConfig.FirstScene);
    }

    private async UniTask LoadSceneAsync(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (operation.isDone == false)
        {
            if (operation.progress >= 0.9f)
            {
                await UniTask.Delay(1000);
                operation.allowSceneActivation = true;
            }

            await UniTask.Yield();
        }
    }
}