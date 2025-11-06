using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float _defaultHeadLocalPitchY = -90f;

    [SerializeField] private float _lookSpeed;
    [SerializeField] private Transform _headBone;
    [SerializeField] private Transform _neckBone;

    [SerializeField] private float _neckRotationMinLimit = -50f;
    [SerializeField] private float _neckRotationMaxLimit = 50f;
    [SerializeField] private float _headPitchMinLimit = 60f;
    [SerializeField] private float _headPitchMaxLimit = 130f;

    private float _bodyRotation;
    private float _neckLocalRotation;
    private float _headLocalPitch;
    private float _accumulatedNeckRotation;

    private void Start()
    {
        _bodyRotation = transform.rotation.y;

        if (_headBone == null)
            throw new ArgumentNullException(nameof(_headBone));

        if (_neckBone == null)
            throw new ArgumentNullException(nameof(_neckBone));

        _neckLocalRotation = _neckBone.localRotation.y;
        _headLocalPitch = _headBone.localRotation.x;
    }

    public void HandleLook(Vector2 direction)
    {
        if (direction.sqrMagnitude <= 0.1f)
            return;

        Look(direction);
    }

    private void Look(Vector2 direction)
    {
        Vector2 lookDelta = _lookSpeed * Time.deltaTime * direction;

        _headLocalPitch -= lookDelta.y;
        _headLocalPitch = Mathf.Clamp(_headLocalPitch, _headPitchMinLimit, _headPitchMaxLimit);

        float desiredNeckRotation = _neckLocalRotation + lookDelta.x;
        _accumulatedNeckRotation += Mathf.Abs(lookDelta.x);

        if (Mathf.Abs(desiredNeckRotation) > _neckRotationMaxLimit ||
            _accumulatedNeckRotation > _neckRotationMaxLimit)
        {
            _bodyRotation += lookDelta.x;
            _accumulatedNeckRotation = 0f;

            _neckLocalRotation = Mathf.MoveTowards(_neckLocalRotation, 0f, _lookSpeed * Time.deltaTime);
        }
        else
        {
            _neckLocalRotation = desiredNeckRotation;
            _neckLocalRotation = Mathf.Clamp(_neckLocalRotation, _neckRotationMinLimit, _neckRotationMaxLimit);
        }

        ApplyRotations();
    }

    private void ApplyRotations()
    {
        transform.rotation = Quaternion.Euler(0f, _bodyRotation, 0f);

        _neckBone.localRotation = Quaternion.Euler(-_neckLocalRotation, 0f, 0f);
        _headBone.localRotation = Quaternion.Euler(_headLocalPitch, _defaultHeadLocalPitchY, 0f);
    }
}