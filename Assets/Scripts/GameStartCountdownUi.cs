using TMPro;
using UnityEngine;

public class GameStartCountdownUi : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start() {
        KitchenGameManger.Instance.OnStateChanged += KitchenGameManger_OnStateChanged;

        Hide();
    }

    private void Update() {
        countdownText.text = Mathf.Ceil(KitchenGameManger.Instance.GetCountdownToStartTimer()).ToString();
    }


    private void KitchenGameManger_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManger.Instance.IsCountdownToStartActive()) Show();
        else Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
