using UnityEngine;

public class InputReader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        if (hit.collider.TryGetComponent<Cube>(out Cube cube))
            cube.OnCubeClicked();
    }
}