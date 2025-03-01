using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button backButton;

    private void Awake() {
        soundEffectsButton.onClick.AddListener(OnClickSoundsEffectsHanlder);
        musicButton.onClick.AddListener(OnClickMusicHanlder);
        backButton.onClick.AddListener(() => Hide());
        Instance = this;
    }

    private void Start() {
        KitchenGameManger.Instance.OnGameResumed += KitchenGameManger_OnGameResumed;

        UpdateVisual();
        Hide();
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
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
