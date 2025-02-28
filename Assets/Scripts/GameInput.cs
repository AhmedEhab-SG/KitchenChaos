using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPause;
    private PlayerInputActions PlayerInputActions;

    private void Awake() {
        Instance = this;

        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Enable();

        PlayerInputActions.Player.Interact.performed += Interact;
        PlayerInputActions.Player.InteractAlternate.performed += InteractAlternate;
        PlayerInputActions.Player.Pause.performed += Pause_preformed;
    }

    private void OnDestroy() {
        PlayerInputActions.Player.Interact.performed -= Interact;
        PlayerInputActions.Player.InteractAlternate.performed -= InteractAlternate;
        PlayerInputActions.Player.Pause.performed -= Pause_preformed;

        PlayerInputActions.Dispose();
    }

    private void Interact(InputAction.CallbackContext e) {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate(InputAction.CallbackContext e) {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_preformed(InputAction.CallbackContext e) {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector2 = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector2.Normalize();
        return inputVector2;
    }
}