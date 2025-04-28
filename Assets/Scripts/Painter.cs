using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Painter : MonoBehaviour
{
    private Renderer _renderer;

    private bool _isPainted = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Paint()
    {
        if (_isPainted)
            return;

        _renderer.material.color = Random.ColorHSV();
        _isPainted = true;
    }
}