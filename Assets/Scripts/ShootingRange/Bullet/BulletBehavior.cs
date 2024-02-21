using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifetime = 5f; // ¬рем€ жизни пули до ее автоматического возвращени€ в пул
    public AudioClip hitSound; // јудиоклип звука попадани€

    private void OnEnable()
    {
        // «апускаю корутину, котора€ автоматически вернет пулю в пул после определенного времени
        StartCoroutine(LifeTimeRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        // ≈сли пул€ столкнулась с целью, обрабатываю столкновение
        if (other.CompareTag("Target"))
        {
            PlayHitSound(); // ¬оспроизвожу звук при попадании
        }
        ReturnToPool();
    }

    private void PlayHitSound()
    {
        // ¬оспроизведение звука попадани€
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }

    private IEnumerator LifeTimeRoutine()
    {
        // ∆ду указанное врем€ жизни, прежде чем вернуть пулю в пул
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        // ƒелаю пулю неактивной, что возвращает ее в пул
        gameObject.SetActive(false);
    }
}
