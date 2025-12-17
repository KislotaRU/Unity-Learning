using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBootstrapper1 : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingCurtain;

    private Dictionary<string, SceneConfiguration> _sceneConfigurations;

    private void Awake()
    {
        LoadNextScene().Forget();
    }

    private async UniTaskVoid LoadNextScene()
    {
        PlayerProgress data = new();
        SceneLoader sceneLoader = new(data);

        _loadingCurtain.Show();

        LoadSceneConfigurations();

        string nextScene = await sceneLoader.LoadNextScene();

        CreateEnemies(nextScene);

        await _loadingCurtain.Hide();
    }

    private void LoadSceneConfigurations() =>
        _sceneConfigurations =
        Resources.LoadAll<SceneConfiguration>("SceneConfigurations").
        ToDictionary(value => value.SceneName, value => value);

    private void CreateEnemies(string sceneName)
    {
        SceneConfiguration sceneConfiguration = _sceneConfigurations[sceneName] ;

        foreach (GameObject enemyPrefab in sceneConfiguration.EnemiesPrefabs) Instantiate(enemyPrefab);
    }
}