using UnityEngine;

public class AudioFadeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speedFading = 0.05f;

    private readonly float _minVolume = 0f;
    private readonly float _maxVolume = 0.7f;

    private bool _isFadingUp = false;
    private bool _isFadingDown = false;

    private void Update()
    {
        if (_isFadingUp)
            ExecuteFadeUp();
        else if (_isFadingDown)
            ExecuteFadeDown();
    }

    public void FadeUp()
    {
        _isFadingUp = true;
        _isFadingDown = false;
    }

    public void FadeDown()
    {
        _isFadingDown = true;
        _isFadingUp = false;
    }

    private void ExecuteFadeUp()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();
        else if (_audioSource.volume < _maxVolume)
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _speedFading * Time.deltaTime);
        else
            _isFadingUp = false;
    }

    private void ExecuteFadeDown()
    {
        if (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _speedFading * Time.deltaTime);
        }
        else
        {
            _audioSource.Stop();
            _isFadingDown = false;
        }
    }
}