using UnityEngine;

public class SnowEffectManager : MonoBehaviour
{
    public ParticleSystem snowParticles;

    public void AdjustSnowEffectBasedOnSpeed(float speed)
    {
        var emission = snowParticles.emission;
        emission.rateOverTime = speed * 20f;
        if (!snowParticles.isPlaying)
            snowParticles.Play();
    }
}