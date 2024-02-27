using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip shootSound;
    public AudioSource boltSound;
    public AudioClip emptyMagazineSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    private bool isPlayingEmptyMagazineSound = false;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
        boltSound.Play();
    }

    public void PlayEmptyMagazineSound()
    {
        if (!isPlayingEmptyMagazineSound)
        {
            audioSource.PlayOneShot(emptyMagazineSound);
            isPlayingEmptyMagazineSound = true;
            StartCoroutine(ResetEmptyMagazineSoundFlag());
        }
    }

    public void PlayReloadSound()
    {
        audioSource.PlayOneShot(reloadSound);
    }

    public bool IsPlayingEmptyMagazineSound()
    {
        return audioSource.isPlaying && audioSource.clip == emptyMagazineSound;
    }

    private IEnumerator ResetEmptyMagazineSoundFlag()
    {
        yield return new WaitForSeconds(emptyMagazineSound.length);
        isPlayingEmptyMagazineSound = false;
    }

}
