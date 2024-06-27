using UnityEngine;

/// <summary>
/// manages the wind sound based on the player's speed.
/// </summary>
public class SkiSoundManager : MonoBehaviour
{
    public AudioSource windSound; // wind sound

    private SpeedController speedController; // reference to the SpeedController component

    void Awake()
    {
        speedController = GetComponent<SpeedController>(); // initialize the SpeedController component
    }

    public void Update()
    {
        float speed = speedController.Speed; // get the current speed
        AdjustWindSound(speed); // adjust the wind sound
    }

    private void AdjustWindSound(float speed)
    {
        float targetVolume = Mathf.Clamp01(speed / speedController.maxSpeed); // set the wind sound volume based on speed
        if (!windSound.isPlaying && targetVolume > 0) // if the sound is not playing but should be
        {
            windSound.Play(); // play the sound
        }
        else if (windSound.isPlaying && targetVolume == 0) // if the sound is playing but should not be
        {
            windSound.Stop(); // stop the sound
        }
        windSound.volume = targetVolume; // set the volume
    }
}