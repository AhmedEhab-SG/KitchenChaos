using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(OnClickPlayButtonHandler);
        quitButton.onClick.AddListener(OnClickQuitButtonHandler);
    }

    private void OnClickPlayButtonHandler() {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void OnClickQuitButtonHandler() {
        Application.Quit();
    }
}
