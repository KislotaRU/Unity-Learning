using UnityEngine;

public interface IMover
{
    float CurrentSpeed { get; }

    void HandleMove(Vector2 direction);
}