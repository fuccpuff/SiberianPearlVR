using UnityEngine;

public class SkiAudioManager : MonoBehaviour
{
    public AudioClip snowSound;
    public AudioClip windSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundBasedOnSpeed(float speed)
    {
        audioSource.clip = speed > 5f ? windSound : snowSound;
        audioSource.volume = Mathf.Clamp01(speed / 10f);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}