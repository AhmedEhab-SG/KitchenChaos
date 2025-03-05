using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryReusltUI : MonoBehaviour {

    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManger.Instance.OnRecipeSuccess += DeliveryManger_OnRecipeSuccess;
        DeliveryManger.Instance.OnRecipeFailed += DeliveryManger_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManger_OnRecipeSuccess(object sender, System.EventArgs e) {
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";

    }

    private void DeliveryManger_OnRecipeFailed(object sender, System.EventArgs e) {
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
