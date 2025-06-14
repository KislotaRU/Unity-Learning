using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Color _color;
    [SerializeField] private Vector3 _scale;
    [Space]
    [SerializeField] private string _newText;
    [SerializeField] private int _duration;
    [SerializeField] private int _delay;

    private string _lastText;
    private Color _lastColor;

    private void Awake()
    {
        _lastText = _text.text;
        _lastColor = _text.color;
    }

    private void Start()
    {
        HandleEffect();
    }

    private void HandleEffect()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(_scale, _duration));
        sequence.AppendInterval(_delay);
        sequence.Append(_text.DOText(_newText, _duration, true, ScrambleMode.Numerals));
        sequence.Join(_text.DOColor(_color, _duration));
        sequence.AppendInterval(_delay);
        sequence.Append(_text.DOText(_lastText, _duration));
        sequence.Join(_text.DOColor(_lastColor, _duration));
    }
}