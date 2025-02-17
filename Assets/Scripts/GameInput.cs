using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    private PlayerInputActions PlayerInputActions;

    private void Awake() {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Enable();

        PlayerInputActions.Player.Interact.performed += Interact;
        PlayerInputActions.Player.InteractAlternate.performed += InteractAlternate;
    }

    private void Interact(InputAction.CallbackContext e) {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate(InputAction.CallbackContext e) {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector2 = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector2.Normalize();
        return inputVector2;
    }
}