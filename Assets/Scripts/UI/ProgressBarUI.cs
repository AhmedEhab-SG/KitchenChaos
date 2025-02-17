using UnityEngine.UI;
using UnityEngine;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;


    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        // check if the game object has the IHasProgress
        if (hasProgress == null) Debug.LogError("hasProgress is null");

        hasProgress.OnProcessChanged += CuttingCounter_OnProcessChanged;

        // init the progress bar as 0 and hide
        barImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnProcessChanged(object sender, IHasProgress.OnProcessChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized >= 1f)
            Hide();
        else
            Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
