using UnityEngine;

public class SoundButton : CommonButton
{
    [SerializeField] private AudioSource _audioSource;

    protected override void HandleClick()
    {
        Debug.Log("Sound");
        _audioSource.Play();
    }
}