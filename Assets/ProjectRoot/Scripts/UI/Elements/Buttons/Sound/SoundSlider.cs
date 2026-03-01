using UnityEngine;
using UnityEngine.Audio;

public class SoundSlider : ActionSlider
{
    private const int MinVolume = -80;
    private const int CoefficientDB = 20;

    [SerializeField] private SoundToggle _toggleSound;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    protected override void HandleValueChanged(float value)
    {
        float currentValue = value == 0 ? MinVolume : Mathf.Log10(value) * CoefficientDB;

        if (_toggleSound.IsMuted)
            return;

        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name.ToString(), currentValue);
    }
}