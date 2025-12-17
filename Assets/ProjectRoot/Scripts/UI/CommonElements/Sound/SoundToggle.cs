using UnityEngine;
using UnityEngine.Audio;

public class SoundToggle : CommonToggle
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    public bool IsMuted { get; private set; }

    protected override void HandleValueChanged(bool value)
    {
        IsMuted = value;

        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name.ToString(), 0);
    }
}