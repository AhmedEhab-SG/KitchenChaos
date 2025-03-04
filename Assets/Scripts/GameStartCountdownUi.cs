using TMPro;
using UnityEngine;

public class GameStartCountdownUi : MonoBehaviour {

    private const string NUMBER_POPUP_TRIGGER = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int prevCountdownNumber;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        KitchenGameManger.Instance.OnStateChanged += KitchenGameManger_OnStateChanged;

        Hide();
    }

    private void Update() {
        int countdownNumber = Mathf.CeilToInt(KitchenGameManger.Instance.GetCountdownToStartTimer());

        countdownText.text = countdownNumber.ToString();

        if (countdownNumber != prevCountdownNumber) {
            animator.SetTrigger(NUMBER_POPUP_TRIGGER);
            SoundManger.Instance.PlayCountdownSound();
            prevCountdownNumber = countdownNumber;
        }
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
