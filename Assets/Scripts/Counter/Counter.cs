using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _stepToChange = 1f;

    private float _scores;
    private bool _isMouseDown;

    private void OnMouseDown()
    {
        if (_isMouseDown == true)
        {
            _isMouseDown = false;
        }
        else
        {
            _isMouseDown = true;
            StartCoroutine(nameof(IncreaseScores));
        }

        Debug.Log($"Êëèê {_isMouseDown}");
    }

    private IEnumerator IncreaseScores()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isMouseDown == true)
        {
            AddScores(_stepToChange);
            yield return wait;
        }
    }

    private void AddScores(float scores)
    {
        if (scores > 0)
        {
            _scores += scores;
            Debug.Log($"Ñ÷¸ò: {_scores}");
        }
    }
}