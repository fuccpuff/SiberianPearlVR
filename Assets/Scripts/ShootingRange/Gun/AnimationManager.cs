using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Animation")]
    public Animator gunAnimator;
    private string shootTriggerName = "Shoot";

    public void PlayShootAnimation()
    {
        gunAnimator.SetTrigger(shootTriggerName);
    }
}
