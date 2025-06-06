using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += HandlePlayButtonClick;
        _endGameScreen.RestartButtonClicked += HandleRestartButtonClick;
        _bird.GameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= HandlePlayButtonClick;
        _endGameScreen.RestartButtonClicked -= HandleRestartButtonClick;
        _bird.GameOver -= HandleGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0;
        _endGameScreen.Open();
    }

    private void HandleRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }
    private void HandlePlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _bird.Reset();
    }
}