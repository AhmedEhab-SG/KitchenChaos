using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
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

    private void KitchenGameManger_OnGameResumed(object sender, System.EventArgs e) {
        Hide();
    }

    private void KitchenGameManger_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
