using UnityEngine;

public class MusicManger : MonoBehaviour {

    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    public static MusicManger Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.3f);
        audioSource.volume = volume;
        Instance = this;
    }

    public void ChangeVolume() {
        volume = volume > 1f ? 0f : volume + 0.1f;
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}