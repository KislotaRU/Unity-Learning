using UnityEngine;

public class SoundButton : ActionButton
{
    [SerializeField] private AudioSource _audioSource;

    protected override void OnClick()
    {
        Debug.Log("Sound");
        _audioSource.Play();
    }
}