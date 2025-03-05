using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour {

    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnProcessChanged += StoveCounter_OnProcessChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProcessChanged(object sender, IHasProgress.OnProcessChangedEventArgs e) {
        float burnShowProgreessAmount = 0.5f;

        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgreessAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}
