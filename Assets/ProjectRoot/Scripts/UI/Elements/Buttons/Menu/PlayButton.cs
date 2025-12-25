using System;
using UnityEngine;
using Zenject;

public class PlayButton : ActionButton
{
    private SceneLoader _sceneLoader;

    [Inject]
    private void Constructor(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        if (_sceneLoader == null)
            throw new ArgumentNullException(nameof(_sceneLoader));
    }

    protected override void OnClick()
    {
        Debug.Log("Play");
        _sceneLoader.LoadSceneWithLoadingScreenAsync(Scenes.Sandbox.ToString()).Forget();
    }
}