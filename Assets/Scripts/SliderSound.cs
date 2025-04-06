using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSound : MonoBehaviour
{
    private const int MinVolume = -80;
    private const int CoefficientDB = 20;

    [Header("Toggle")]
    [SerializeField] private ToggleSound _toggleSound;

    [Header("Sound")]
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        if (_toggleSound == null)
            enabled = false;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleTranslateVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleTranslateVolume);
    }

    public void HandleTranslateVolume(float targetVolume)
    {
        float currentVolume = targetVolume == 0 ? MinVolume : Mathf.Log10(targetVolume) * CoefficientDB;

        if (_toggleSound.IsMuted)
            return;

        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name.ToString(), currentVolume);
    }
}