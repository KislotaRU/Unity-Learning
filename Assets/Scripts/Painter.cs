using UnityEngine;

public class Painter : MonoBehaviour
{
    public void Paint(Renderer renderer)
    {
        renderer.material.color = Random.ColorHSV();
    }
}