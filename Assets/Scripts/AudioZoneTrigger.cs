using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    [SerializeField] private AudioFadeController _audioFadeController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief _))
        {
            _audioFadeController.FadeUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief _))
        {
            _audioFadeController.FadeDown();
        }
    }
}