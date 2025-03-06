using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    [SerializeField] private AudioVolumeEffect _audioVolumeEffect;

    private readonly float _minVolume = 0.0f;
    private readonly float _maxVolume = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief _))
            _audioVolumeEffect.Translate(_maxVolume);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief _))
            _audioVolumeEffect.Translate(_minVolume);
    }
}