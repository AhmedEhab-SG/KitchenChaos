using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAltText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyInteractGamepadText;
    [SerializeField] private TextMeshProUGUI keyInteractAltGamepadText;
    [SerializeField] private TextMeshProUGUI keyPauseGamepadText;


    private void Start() {
        UpdateVisual();

        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManger.Instance.OnStateChanged += KitchenGameManger_OnStateChanged;

        Show();
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void KitchenGameManger_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManger.Instance.IsCountdownToStartActive()) Hide();

    }

    private void UpdateVisual() {

        keyMoveUpText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up)}";

        keyMoveDownText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down)}";

        keyMoveLeftText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left)}";

        keyMoveRightText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right)}";

        keyInteractText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Interact)}";

        keyInteractAltText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate)}";

        keyPauseText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Pause)}";

        keyInteractGamepadText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact)}";

        keyInteractAltGamepadText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_Alternate)}";

        keyPauseGamepadText.text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause)}";
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
