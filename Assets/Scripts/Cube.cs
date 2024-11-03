using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField, Range(0f, 1f)] private float _chanceToSpawn = 1.0f;
    [Space]
    [SerializeField] private Painter _painter;
    [SerializeField] private Exploder _exploder;

    public float ChanceToSpawn => _chanceToSpawn;

    public void Initialize(Vector3 scale, float chanceToSpawn)
    {
        transform.localScale = scale;
        _chanceToSpawn = chanceToSpawn;

        Paint();
    }

    public void OnCubeClicked()
    {
        DestroyProcess();

        if (TrySpawn() == false)
            Explode();
    }

    private void DestroyProcess()
    {
        Destroy(gameObject);
    }

    private bool TrySpawn()
    {
        float currentChanceClone = Random.value;

        if (_chanceToSpawn >= currentChanceClone)
        {
            _spawner.Spawn(GetComponent<Cube>());
            return true;
        }

        return false;
    }

    private void Paint()
    {
        Renderer renderer = GetComponent<Renderer>();

        _painter.Paint(renderer);
    }

    private void Explode()
    {
        _exploder.Explode(transform.position, transform.localScale);
    }
}