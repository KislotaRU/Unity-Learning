using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    //[SerializeField] private float _chanceToSpawn = 1.0f;

    //[SerializeField] private InputReader _inputReader;
    //[SerializeField] private Spawner _spawner;
    //[SerializeField] private Exploder _exploder;

    //private void OnEnable()
    //{
    //    _inputReader.Clicked += Spawn;
    //    _inputReader.Clicked += Explode;
    //    _inputReader.Clicked += DestroyObject;
    //}

    //private void OnDisable()
    //{
    //    _inputReader.Clicked -= Spawn;
    //    _inputReader.Clicked -= Explode;
    //    _inputReader.Clicked -= DestroyObject;
    //}

    //private IEnumerator DestroyCoroutine()
    //{
    //    float duration = 1f;
    //    float elapsedTime = 0f;
    //    float normalizedTime;
    //    Vector3 scaleStart = transform.localScale;
    //    Vector3 scaleEnd = Vector3.zero;

    //    while (elapsedTime <= duration)
    //    {
    //        normalizedTime = elapsedTime / duration;
    //        normalizedTime = Mathf.Clamp(normalizedTime, 0f, 1f);
    //        transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, normalizedTime);
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    Destroy(gameObject);
    //}

    //private void DestroyObject()
    //{
    //    StartCoroutine(nameof(DestroyCoroutine));
    //}

    //public void Spawn()
    //{
        
    //    return;

    //    Renderer renderer = GetComponent<Renderer>();

    //    int reductionFactorChanceToSpawn = 2;
    //    int reductionFactorScale = 2;
    //    float currentChanceClone;

    //    renderer.material.color = Random.ColorHSV();

    //    Vector3 scaleClone = transform.localScale / reductionFactorChanceToSpawn;

    //    currentChanceClone = Random.value;       

    //    if (_chanceToSpawn < currentChanceClone)
    //        return;

    //    _chanceToSpawn /= reductionFactorScale;
    //    _spawner.Spawn(gameObject, scaleClone);
    //}

    //public void Explode()
    //{

    //}

    //public void Paint()
    //{

    //}
}