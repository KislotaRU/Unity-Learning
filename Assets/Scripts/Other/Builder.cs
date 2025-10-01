using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Facility _prefab;
    [SerializeField] private BuildPreview _buildPreview;
    [SerializeField] private Transform _container;

    private RaycastHit _raycastHit;
    private Facility _facility;

    public Vector3 BuildPosition => _buildPreview.transform.position + Vector3.up;
    public bool IsPlacing { get; private set; }
    public bool IsPlaced { get; private set; }

    private void Start()
    {
        _camera = Camera.main;

        DisablePlacement();
    }

    private void Update()
    {
        if (IsPlacing == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandlePlacement();
            }
        }
        else
        {
            UpdatePlacementPosition();
        }

        if (Input.GetMouseButtonDown(1))
            Place();

        if (Input.GetMouseButtonDown(2))
            DisablePlacement();
    }

    public void EnablePlacement()
    {
        IsPlacing = true;
        
        if (_buildPreview.IsActive == false)
            _buildPreview.Enable();
    }

    public void DisablePlacement()
    {
        IsPlacing = false;

        if (_buildPreview.IsActive)
            _buildPreview.Disable();

        _facility?.SetConstructionMode(false, this);
    }

    public Facility Build()
    {
        DisablePlacement();

        Facility facility = Instantiate(_prefab, _buildPreview.transform.position, _buildPreview.transform.rotation);

        facility.Initialize();

        IsPlaced = false;

        return facility;
    }

    private void UpdatePlacementPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _raycastHit) == false)
            return;

        if (_raycastHit.transform.TryGetComponent(out Floor _) == false)
            return;

        _buildPreview.transform.position = _raycastHit.point;
    }

    private void HandlePlacement()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _raycastHit) == false)
            return;

        if (_raycastHit.transform.TryGetComponent(out Facility facility) == false)
            return;

        if (facility.CanBuild() == false)
            return;

        _facility = facility;

        _facility.SetConstructionMode(false, this);

        EnablePlacement();
    }

    private void Place()
    {
        IsPlaced = true;
        IsPlacing = false;

        _facility.SetConstructionMode(true, this);
    }
}