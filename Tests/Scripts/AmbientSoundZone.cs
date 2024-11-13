using UnityEngine;

public class AmbientSoundZone : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AmbientSound _ambientSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AmbientListener _))
            _ambientSound.SetAmbient(_clip);
    }
}