using System.Collections.Generic;
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
        if (TrySpawn(out List<Rigidbody> rigidbodies) == false)
            return;

        Explode(rigidbodies);

        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private bool TrySpawn(out List<Rigidbody> rigidbodies)
    {
        float currentChanceClone = Random.value;
        rigidbodies = null;

        if (_chanceToSpawn >= currentChanceClone)
            rigidbodies = _spawner.Spawn(GetComponent<Cube>());

        return rigidbodies != null;
    }

    private void Paint()
    {
        Renderer renderer = GetComponent<Renderer>();

        _painter.Paint(renderer);
    }

    private void Explode(List<Rigidbody> rigidbodies)
    {
        _exploder.Explode(rigidbodies);
    }
}