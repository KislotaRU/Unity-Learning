using System;
using UnityEngine;
using Zenject;

public class Rotator : MonoBehaviour, IRotator
{
    private const float MinSqrMagnitude = 0.1f;

    [Header("Rigging Settings")]
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private float _lookDistance = 3f;
    [SerializeField] private float _headHeight = 1.6f;

    [Header("Look Settings")]
    [SerializeField] private float _lookSpeed = 2f;
    [SerializeField] private float _bodyRotationSpeed = 1f;

    [Header("Rotation Limits")]
    [SerializeField] private float _neckRotationLimit = 50f;
    [SerializeField] private float _headPitchLimit = 50f;
    [SerializeField] private float _bodyRotationThreshold = 0.8f;

    private float _currentYaw;
    private float _currentPitch;
    private Vector2 _smoothLookInput;
    private float _accumulatedYaw;

    private Vector3 HeadPosition => transform.position + Vector3.up * _headHeight;

    [Inject]
    private void Cunstruct(Transform lookTarget)
    {
        _lookTarget = lookTarget;
    }

    private void Awake()
    {
        if (_lookTarget == null)
            throw new ArgumentNullException(nameof(_lookTarget));
    }

    private void Start()
    {
        ResetLookTarget();
    }

    public void HandleLook(Vector2 direction)
    {
        if ((direction.sqrMagnitude >= MinSqrMagnitude) == false)
            return;

        _smoothLookInput = Vector2.Lerp(_smoothLookInput, direction, _lookSpeed * Time.deltaTime);

        Look(_smoothLookInput);
    }

    private void Look(Vector2 direction)
    {
        Vector2 angleDelta = _lookSpeed * Time.deltaTime * direction;

        _currentYaw += angleDelta.x;
        _currentPitch -= angleDelta.y;

        _accumulatedYaw += Mathf.Abs(angleDelta.x);

        _currentYaw = Mathf.Clamp(_currentYaw, -_neckRotationLimit, _neckRotationLimit);
        _currentPitch = Mathf.Clamp(_currentPitch, -_headPitchLimit, _headPitchLimit);

        if (ShouldRotateBody())
            RotateBodySmoothly(angleDelta.x);

        UpdateLookTarget();
    }

    private bool ShouldRotateBody()
    {
        return Mathf.Abs(_currentYaw) >= _neckRotationLimit * _bodyRotationThreshold ||
               _accumulatedYaw >= _neckRotationLimit;
    }

    private void RotateBodySmoothly(float inputDelta)
    {
        float bodyRotationAmount = Mathf.Sign(_currentYaw) * _bodyRotationSpeed * Time.deltaTime;
        transform.Rotate(0f, bodyRotationAmount, 0f);

        _currentYaw -= bodyRotationAmount;
        _accumulatedYaw = Mathf.Max(0f, _accumulatedYaw - Mathf.Abs(bodyRotationAmount));

        _currentYaw = Mathf.Clamp(_currentYaw, -_neckRotationLimit, _neckRotationLimit);
    }

    private void UpdateLookTarget()
    {
        Quaternion headLocalRotation = Quaternion.Euler(_currentPitch, _currentYaw, 0f);

        Vector3 lookDirection = transform.rotation * headLocalRotation * Vector3.forward;

        Vector3 targetPosition = HeadPosition + lookDirection * _lookDistance;

        _lookTarget.position = targetPosition;
    }

    private void ResetLookTarget()
    {
        _currentYaw = 0f;
        _currentPitch = 0f;
        _smoothLookInput = Vector2.zero;
        _accumulatedYaw = 0f;

        _lookTarget.position = HeadPosition + transform.forward * _lookDistance;
    }
}