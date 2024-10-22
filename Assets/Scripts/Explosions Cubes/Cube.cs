using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action MouseClicked;

    private void OnMouseDown()
    {
        MouseClicked?.Invoke();
    }
}