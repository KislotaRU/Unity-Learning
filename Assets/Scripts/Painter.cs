using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Painter : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Paint()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}