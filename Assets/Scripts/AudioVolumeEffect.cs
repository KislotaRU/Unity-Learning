using System.Collections;
using UnityEngine;

public class AudioVolumeEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speedTranslating = 0.1f;

    private IEnumerator TranslatingSmooth(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedTranslating * Time.deltaTime);

            yield return null;
        }
    }

    public void Translate(float targetVolume)
    {
        StopAllCoroutines();
        StartCoroutine(TranslatingSmooth(Mathf.Clamp01(targetVolume)));
    }
}