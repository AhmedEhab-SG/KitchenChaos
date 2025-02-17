using UnityEngine;

public class SoundManger : MonoBehaviour {

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Start() {
        DeliveryManger.Instance.OnRecipeFailed += DeliveryManger_OnRecipeFailed;
        DeliveryManger.Instance.OnRecipeCompleted += DeliveryManger_OnRecipeCompleted;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
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

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }
}
