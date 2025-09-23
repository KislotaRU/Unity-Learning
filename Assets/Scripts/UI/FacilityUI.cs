using UnityEngine;

public class FacilityUI : MonoBehaviour
{
    [SerializeField] private Facility _facility;

    [SerializeField] private StatValueUI _resourcesCapacityUI;

    private void Awake()
    {
        if (_facility == null)
        {
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        _resourcesCapacityUI.SetStat(_facility.ResourcesCapacity);
    }
}