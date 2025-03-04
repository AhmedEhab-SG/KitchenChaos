using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnCounterChanged;
        stoveCounter.OnProcessChanged += StoveCounter_OnProcessChanged;
    }

    private void StoveCounter_OnProcessChanged(object sender, IHasProgress.OnProcessChangedEventArgs e) {
        float burnShowProgreessAmount = 0.5f;

        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgreessAmount;

    }


    private void StoveCounter_OnCounterChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound) audioSource.Play();
        else audioSource.Pause();
    }

    private void Update() {
        if (!playWarningSound) return;

        warningSoundTimer += Time.deltaTime;
        if (warningSoundTimer <= 0) {
            float warningSoundTimerMax = 0.2f;
            warningSoundTimer = warningSoundTimerMax;

            SoundManger.Instance.PlayWarningSound(stoveCounter.transform.position);
        }

    }
}
