using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public Vector2 MoveDirection { get; private set; }
    public bool IsJumping { get; private set; }

    private void Update()
    {
        MoveDirection = GetDirection();
        IsJumping = Input.GetButtonDown(Jump);
    }

    private Vector2 GetDirection()
    {
        float positionX = Input.GetAxis(Horizontal);

        return new Vector2(positionX, 0);
    }
}