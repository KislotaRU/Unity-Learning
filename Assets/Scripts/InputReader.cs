using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] GameObject _prefabTrigger;

    public event Action Clicked;

    private void OnMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.Log("0");

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        Debug.Log("1");

        if (_prefabTrigger.GetType().Name != hit.GetType().Name)
            return;

        Debug.Log("2");

        //Clicked?.Invoke();
    }
}