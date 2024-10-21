using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float _delay = 1.0f;
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _position = new Vector3(0.0f, 2.0f, 0.0f);

    private void Start()
    {
        Invoke(nameof(SpawnObject), _delay);
    }

    private void SpawnObject()
    {
        Instantiate(_target, _position, Quaternion.identity);
    }
}