using UnityEngine;
using UnityEngine.Audio;

public class PanelSettings : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const string UIVolume = "UIVolume";

    private const int CoefficientDB = 20;
    private const int MinMasterVolume = -80;

    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private float _currentMasterVolume = 0;
    private bool _isMutedSound = false;

    public void MuteSound(bool isEnabled)
    {
        if (isEnabled)
            _audioMixerGroup.audioMixer.SetFloat(MasterVolume, MinMasterVolume);
        else
            _audioMixerGroup.audioMixer.SetFloat(MasterVolume, _currentMasterVolume);

        _isMutedSound = isEnabled;
    }

    public void TranslateVolumeMasterMusic(float targetVolume)
    {
        _currentMasterVolume = Mathf.Log10(targetVolume) * CoefficientDB;

        if (_isMutedSound)
            return;

        _audioMixerGroup.audioMixer.SetFloat(MasterVolume, _currentMasterVolume);
    }

    public void TranslateVolumeMusic(float targetVolume)
    {
        if (_isMutedSound)
            return;

        _audioMixerGroup.audioMixer.SetFloat(MusicVolume, Mathf.Log10(targetVolume) * CoefficientDB);
    }

    public void TranslateVolumeSound(float targetVolume)
    {
        if (_isMutedSound)
            return;

        _audioMixerGroup.audioMixer.SetFloat(UIVolume, Mathf.Log10(targetVolume) * CoefficientDB);
    }
}