using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleSound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleSound);
    }

    public void HandleSound()
    {
        _audioSource.Play();
    }
}