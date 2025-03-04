using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        optionsButton.onClick.AddListener(OnClickOptionsButtonHandler);
    }

    private void Start() {
        KitchenGameManger.Instance.OnGamePaused += KitchenGameManger_OnGamePaused;

        KitchenGameManger.Instance.OnGameResumed += KitchenGameManger_OnGameResumed;

        Hide();
    }

    private void OnMainMenuButtonClicked() {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void OnResumeButtonClicked() {
        KitchenGameManger.Instance.TogglePauseGame();
    }

    private void OnClickOptionsButtonHandler() {
        Hide();
        OptionsUI.Instance.Show(Show);
    }

    private void KitchenGameManger_OnGameResumed(object sender, System.EventArgs e) {
        Hide();
    }

    private void KitchenGameManger_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
