using System;
using UnityEngine;

public class KitchenGameManger : MonoBehaviour {

    public static KitchenGameManger Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 0;
    private float gamePlayingTimerMax = 100f;
    private bool isGamePaused = false;


    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPause += GameInput_OnPause;
        GameInput.Instance.OnInteract += GameInput_OnInteract;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:

                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f) {
                    state = State.GamePlaying;

                    gamePlayingTimer = gamePlayingTimerMax;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f) {
                    state = State.GameOver;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
    }

    private void GameInput_OnPause(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void GameInput_OnInteract(object sender, EventArgs e) {
        if (state != State.WaitingToStart) return;

        state = State.CountdownToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);

    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    // revesed the timer to be from 1 to 0
    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            OnGameResumed?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
        }
    }
}
