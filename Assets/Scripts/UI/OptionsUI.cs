using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button backButton;

    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button GamepadInteractButton;
    [SerializeField] private Button GamepadInteractAltButton;
    [SerializeField] private Button GamepadPauseButton;
    [SerializeField] private Transform pressToRebindKeyTransfom;

    private Action onCloseButtonAction;

    private void Awake() {
        soundEffectsButton.onClick.AddListener(OnClickSoundsEffectsHanlder);
        musicButton.onClick.AddListener(OnClickMusicHanlder);
        backButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });

        MoveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Up));
        MoveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Down));
        MoveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Left));
        MoveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Right));
        InteractButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact));
        InteractAltButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact_Alternate));
        PauseButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Pause));
        GamepadInteractButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Gamepad_Interact));
        GamepadInteractAltButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Gamepad_Interact_Alternate));
        GamepadPauseButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Gamepad_Pause));

        Instance = this;
    }

    private void Start() {
        KitchenGameManger.Instance.OnGameResumed += KitchenGameManger_OnGameResumed;

        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void OnClickSoundsEffectsHanlder() {
        SoundManger.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void OnClickMusicHanlder() {
        MusicManger.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void KitchenGameManger_OnGameResumed(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Sound Effects: {Mathf.Round(SoundManger.Instance.GetVolume() * 10f)}";
        musicButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Music: {Mathf.Round(MusicManger.Instance.GetVolume() * 10f)}";

        MoveUpButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up)}";

        MoveDownButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down)}";

        MoveLeftButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left)}";

        MoveRightButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right)}";

        InteractButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Interact)}";

        InteractAltButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate)}";

        PauseButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Pause)}";

        GamepadInteractButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact)}";

        GamepadInteractAltButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_Alternate)}";

        GamepadPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause)}";
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void ShowPressToRebindKey() {
        pressToRebindKeyTransfom.gameObject.SetActive(true);
    }

    public void HidePressToRebindKey() {
        pressToRebindKeyTransfom.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindingBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
