using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;


    private void Start() {
        stoveCounter.OnProcessChanged += StoveCounter_OnProcessChanged;

        Hide();
    }

    private void StoveCounter_OnProcessChanged(object sender, IHasProgress.OnProcessChangedEventArgs e) {
        float burnShowProgreessAmount = 0.5f;

        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgreessAmount;

        if (show) Show();
        else Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
