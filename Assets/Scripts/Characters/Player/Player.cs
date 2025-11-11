using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private EntityConfigurator _configurator;

    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerController _playerController;

    private IHealth _health;

    private void Awake()
    {
        if (_configurator == null)
            throw new ArgumentNullException(nameof(_configurator));

        if (TryGetComponent(out PlayerAnimator playerAnimator) == false)
            throw new ArgumentNullException(nameof(playerAnimator));

        if (TryGetComponent(out PlayerController playerController) == false)
            throw new ArgumentNullException(nameof(playerController));

        _playerAnimator = playerAnimator;
        _playerController = playerController;

        _health = new Health(_configurator.MaxValueHealth);
    }

    private void OnEnable()
    {
        _health.Devastated += HandleDied;
    }

    private void OnDisable()
    {
        _health.Devastated -= HandleDied;
    }

    private void Update()
    {
        _playerController.UpdateInput();
        _playerAnimator.SetParametrs(_playerController.SpeedMovement);
    }

    private void HandleDied()
    {
        Debug.Log("Died");
    }
}