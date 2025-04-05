using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSound : MonoBehaviour
{
    private const int CoefficientDB = 20;

    [Header("Toggle")]
    [SerializeField] private ToggleSound _toggleSound;

    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TypesVolume _typeVolume;

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
        if (_toggleSound.IsMuted)
            return;

        _audioMixer.SetFloat(_typeVolume.ToString(), Mathf.Log10(targetVolume) * CoefficientDB);
    }

    public void HandleSound()
    {
        _audioSource.Play();
    }
}