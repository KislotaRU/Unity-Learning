using DG.Tweening;
using UnityEngine;

public class ColorTo : MonoBehaviour
{
    [SerializeField] private Material _material;
    [Space]
    [SerializeField] private Color _color;
    [SerializeField] private float _duration;

    private void Start()
    {
        HandleColor();
    }

    private void HandleColor()
    {
        _material.DOColor(_color, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}