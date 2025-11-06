using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        if (TryGetComponent(out PlayerController playerController) == false)
            throw new ArgumentNullException(nameof(playerController));

        _playerController = playerController;
    }

    private void Update()
    {
        _playerController.UpdateInput();
    }
}