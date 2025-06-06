using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}