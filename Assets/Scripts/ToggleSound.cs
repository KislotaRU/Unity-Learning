using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleSound : MonoBehaviour
{
    private const int MinVolume = -80;
    private const int MaxVolume = 0;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private TypesVolume _typeVolume;

    private Toggle _toggle;
    
    public bool IsMuted { get; private set; }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(HandleMute);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(HandleMute);
    }

    private void HandleMute(bool isEnabled)
    {
        float currentVolume = isEnabled ? MinVolume : MaxVolume;

        HandleSound();

        _audioMixer.SetFloat(_typeVolume.ToString(), currentVolume);
        IsMuted = isEnabled;
    }

    private void HandleSound()
    {
        _audioSource.Play();
    }
}