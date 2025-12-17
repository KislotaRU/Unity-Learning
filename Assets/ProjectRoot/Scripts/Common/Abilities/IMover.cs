using UnityEngine;

public interface IMover
{
    float CurrentSpeed { get; }

    void HandleMove(Vector3 direction);
}