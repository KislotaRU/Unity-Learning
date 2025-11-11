using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        if (TryGetComponent(out PlayerAnimator playerAnimator) == false)
            throw new ArgumentNullException(nameof(playerAnimator));

        if (TryGetComponent(out PlayerController playerController) == false)
            throw new ArgumentNullException(nameof(playerController));

        _playerAnimator = playerAnimator;
        _playerController = playerController;
    }

    private void Update()
    {
        _playerController.UpdateInput();
        _playerAnimator.SetParametrs(_playerController.SpeedMovement);
    }
}