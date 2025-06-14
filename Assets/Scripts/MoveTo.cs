using DG.Tweening;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _duration;

    private void Start()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        transform.DOMove(_position, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}