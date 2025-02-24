using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start() {
        KitchenGameManger.Instance.OnStateChanged += KitchenGameManger_OnStateChanged;

        Hide();
    }

    private void KitchenGameManger_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManger.Instance.IsGameOver()) {
            Show();

            recipesDeliveredText.text = DeliveryManger.Instance.GetSuccessfulRecipesAmount().ToString();
        } else Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
