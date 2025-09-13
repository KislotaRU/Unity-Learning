using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<Vector3> _targets;
    [SerializeField] private Scanner _scanner;

    private void Awake()
    {
        _units = new List<Unit>();
        _targets = new List<Vector3>();
    }

    public void FixedUpdate()
    {
        HandleScanner();
    }

    private void HandleScanner()
    {
        if (_targets.Count != 0)
            return;

        _targets = _scanner.GetTargets();
    }
}