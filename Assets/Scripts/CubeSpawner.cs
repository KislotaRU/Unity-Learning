using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubePool;
    private BoxCollider _boxCollider;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 5;

    private float _spawnDelay = 1f;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(createFunc: () => CreateManually(),
                                     actionOnGet: (cube) => ActionOnGet(cube),
                                     actionOnRelease: (cube) => cube.gameObject.SetActive(false),
                                     actionOnDestroy: (cube) => Destroy(cube),
                                     collectionCheck: true,
                                     defaultCapacity: _poolCapacity,
                                     maxSize: _poolMaxSize);
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();

        StartCoroutine(nameof(SpawningWithDelay));
    }

    private IEnumerator SpawningWithDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            _cubePool.Get();

            yield return delay;
        }
    }

    public Cube CreateManually()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.Initialize(_cubePool);

        return cube;
    }

    private void ActionOnGet(Cube cube)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-_boxCollider.size.x / 2, _boxCollider.size.x / 2),
                                             transform.position.y,
                                             Random.Range(-_boxCollider.size.x / 2, _boxCollider.size.z / 2));

        cube.transform.rotation = Quaternion.identity;

        if (cube.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.velocity = Vector3.zero;

        cube.transform.Translate(randomPosition);
        cube.gameObject.SetActive(true);
    }
}