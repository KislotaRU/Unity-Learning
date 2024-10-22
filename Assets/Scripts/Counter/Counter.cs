using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _score = 1f;

    private float _scores;
    private bool _isMouseDown;

    private void OnMouseDown()
    {
        if (_isMouseDown)
        {
            _isMouseDown = false;
        }
        else
        {
            _isMouseDown = true;
            StartCoroutine(nameof(IncreaseScores));
        }
    }

    private IEnumerator IncreaseScores()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isMouseDown)
        {
            AddScores();
            yield return wait;
        }
    }

    private void AddScores()
    {
        if (_score > 0)
        {
            _scores += _score;
            Debug.Log($"Scores: {_scores}");
        }
    }
}