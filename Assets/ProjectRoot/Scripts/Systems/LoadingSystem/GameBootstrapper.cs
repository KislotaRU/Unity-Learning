using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private GameConfig _gameConfig;
    private SceneLoader _sceneLoader;
    private LoadingScreen _loadingScreen;

    [Inject]
    private void Constructor(GameConfig gameConfig, SceneLoader sceneLoader, LoadingScreen loadingScreen)
    {
        _gameConfig = gameConfig;
        _sceneLoader = sceneLoader;
        _loadingScreen = loadingScreen;
    }

    private void Awake()
    {
        if (_gameConfig == null)
            throw new ArgumentNullException(nameof(_gameConfig));

        if (_sceneLoader == null)
            throw new ArgumentNullException(nameof(_sceneLoader));

        if (_loadingScreen == null)
            throw new ArgumentNullException(nameof(_loadingScreen));
    }

    private void Start()
    {
        LoadFirstSceneAsync().Forget();
    }

    private async UniTaskVoid LoadFirstSceneAsync()
    {
        _loadingScreen.SetName(_gameConfig.FirstScene.ToString());

        await _loadingScreen.Show();

        IProgress<float> progress = Progress.Create<float>(value => _loadingScreen.SetProgress(value));

        _loadingScreen.SetMessage("Starting game...");
        progress.Report(0.24f);
        await UniTask.Delay(1000);
        await _sceneLoader.LoadSceneAsync(_gameConfig.FirstScene.ToString());

        _loadingScreen.SetMessage($"Completion...");
        progress.Report(1.0f);
        await UniTask.Delay(1000);

        await _loadingScreen.Hide();
    }
}