using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.1f;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0) {
            footstepTimer = footstepTimerMax;

            float volume = 1f;
            if (player.IsWalking())
                SoundManger.Instance.PlayFootstepsSound(transform.position, volume);
        }
    }
}
