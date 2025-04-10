using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);
    private const string Fire2 = nameof(Fire2);

    public Vector2 MoveDirection { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsVampiring { get; private set; }

    private void Update()
    {
        MoveDirection = GetDirection();
        IsJumping = Input.GetButtonDown(Jump);
        IsAttacking = Input.GetButtonDown(Fire1);
        IsVampiring = Input.GetButtonDown(Fire2);
    }

    private Vector2 GetDirection()
    {
        float positionX = Input.GetAxis(Horizontal);

        return new Vector2(positionX, 0);
    }
}