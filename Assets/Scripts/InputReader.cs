using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly int _numberButton = 0;

    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_numberButton) == false)
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        if (hit.collider.TryGetComponent(out Cube cube))
            cube.OnCubeClicked();
    }
}