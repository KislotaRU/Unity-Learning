using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);

    public bool IsFlying { get; private set; }
    public bool IsShooting { get; private set; }

    private void Update()
    {
        IsFlying = Input.GetButtonDown(Jump);
        IsShooting = Input.GetButtonDown(Fire1);
    }
}