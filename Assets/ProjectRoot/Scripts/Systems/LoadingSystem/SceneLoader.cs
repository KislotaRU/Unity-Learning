using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly PlayerProgress _playerProgress;

    public SceneLoader(PlayerProgress playerProgress)
    {
        _playerProgress = playerProgress;
    }

    public async UniTask<string> LoadNextScene()
    {
        string nextScene = _playerProgress.GetNextScene();

        await SceneManager.LoadSceneAsync(nextScene);

        return nextScene;
    }

    public async UniTask LoadSceneAsync(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (operation.isDone == false)
        {
            if (operation.progress >= 0.9f)
            {
                await UniTask.Delay(500);
                operation.allowSceneActivation = true;
            }

            await UniTask.Yield();
        }
    }
}