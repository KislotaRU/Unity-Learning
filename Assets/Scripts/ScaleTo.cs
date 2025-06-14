using DG.Tweening;
using UnityEngine;

public class ScaleTo : MonoBehaviour
{
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _duration;

    private void Start()
    {
        HandleScale();
    }

    private void HandleScale()
    {
        transform.DOScale(_scale, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}