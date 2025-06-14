using DG.Tweening;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _duration;

    private void Start()
    {
        HandleRotate();
    }

    private void HandleRotate()
    {
        transform.DORotate(_rotation, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}