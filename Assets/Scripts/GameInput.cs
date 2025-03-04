using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour {

    private const string PLAYER_PREFS_BINDINGs = "InputBindings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPause;
    public event EventHandler OnBindingRebind;

    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alternate,
        Pause,
        Gamepad_Interact,
        Gamepad_Interact_Alternate,
        Gamepad_Pause
    }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGs))
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGs));

        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate;
        playerInputActions.Player.Pause.performed += Pause_preformed;
    }

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate;
        playerInputActions.Player.Pause.performed -= Pause_preformed;

        playerInputActions.Dispose();
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
        Vector2 inputVector2 = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector2.Normalize();
        return inputVector2;
    }

    public string GetBindingText(Binding binding) {
        switch (binding) {
            case Binding.Move_Up:
                return playerInputActions.Player.Move.GetBindingDisplayString(1);
            case Binding.Move_Down:
                return playerInputActions.Player.Move.GetBindingDisplayString(2);
            case Binding.Move_Left:
                return playerInputActions.Player.Move.GetBindingDisplayString(3);
            case Binding.Move_Right:
                return playerInputActions.Player.Move.GetBindingDisplayString(4);
            case Binding.Interact:
                return playerInputActions.Player.Interact.GetBindingDisplayString(0);
            case Binding.Interact_Alternate:
                return playerInputActions.Player.InteractAlternate.GetBindingDisplayString(0);
            case Binding.Pause:
                return playerInputActions.Player.Pause.GetBindingDisplayString(0);
            case Binding.Gamepad_Interact:
                return playerInputActions.Player.Interact.GetBindingDisplayString(1);
            case Binding.Gamepad_Interact_Alternate:
                return playerInputActions.Player.InteractAlternate.GetBindingDisplayString(1);
            case Binding.Gamepad_Pause:
                return playerInputActions.Player.Pause.GetBindingDisplayString(1);
            default:
                return "N/A";
        }
    }

    public void RebindingBinding(Binding binding, Action onActionRebound) {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding) {
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;

            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;

            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;

            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;

            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;

            case Binding.Interact_Alternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;

            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;

            case Binding.Gamepad_Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;

            case Binding.Gamepad_Interact_Alternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;

            case Binding.Gamepad_Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;

            default:
                return;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(operation => {
            operation.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGs, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }
}