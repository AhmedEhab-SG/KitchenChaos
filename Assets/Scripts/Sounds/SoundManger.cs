using UnityEngine;

public class SoundManger : MonoBehaviour {

    private const string SOUND_VOLUME_KEY = "SoundVolume";

    public static SoundManger Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake() {
        volume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, 1f);
        Instance = this;
    }

    private void Start() {
        DeliveryManger.Instance.OnRecipeFailed += DeliveryManger_OnRecipeFailed;
        DeliveryManger.Instance.OnRecipeCompleted += DeliveryManger_OnRecipeCompleted;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        Player.Instance.OnPickedSomething += Player_OnPickedSomething;

        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;

        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
    }

    private void DeliveryManger_OnRecipeFailed(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }


    private void DeliveryManger_OnRecipeCompleted(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.chop, (sender as CuttingCounter).transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectDrop, (sender as BaseCounter).transform.position);
    }

    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.trash, (sender as TrashCounter).transform.position);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volumeMulitplier = 1f) {
        PlaySound(audioClipRefsSO.footstep, position, volumeMulitplier * volume);
    }

    public void PlayCountdownSound() {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position) {
        PlaySound(audioClipRefsSO.warning, position);
    }

    public void ChangeVolume() {
        volume = volume > 1f ? 0f : volume + 0.1f;

        PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
