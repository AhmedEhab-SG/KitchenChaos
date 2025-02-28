using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(OnClickPlayButtonHandler);
        quitButton.onClick.AddListener(OnClickQuitButtonHandler);

        // resets the time scale to normal
        Time.timeScale = 1f;
    }

    private void OnClickPlayButtonHandler() {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void OnClickQuitButtonHandler() {
        Application.Quit();
    }
}
