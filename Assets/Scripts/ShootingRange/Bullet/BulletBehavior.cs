using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifetime = 5f; // ����� ����� ���� �� �� ��������������� ����������� � ���
    public AudioClip hitSound; // ��������� ����� ���������

    private void OnEnable()
    {
        // �������� ��������, ������� ������������� ������ ���� � ��� ����� ������������� �������
        StartCoroutine(LifeTimeRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ���� ����������� � �����, ����������� ������������
        if (other.CompareTag("Target"))
        {
            PlayHitSound(); // ������������ ���� ��� ���������
        }
        ReturnToPool();
    }

    void OnCollisionEnter(Collision collision)
    {
        // ����� � ������� ����� �������, � ������� ��������� ������������
        Debug.Log($"Bullet hit: {collision.gameObject.name}");

        // ����������� ���� ��� ������ ������ �� ��������� ������������
        gameObject.SetActive(false);
    }


    private void PlayHitSound()
    {
        // ��������������� ����� ���������
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }

    private IEnumerator LifeTimeRoutine()
    {
        // ��� ��������� ����� �����, ������ ��� ������� ���� � ���
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        // ����� ���� ����������, ��� ���������� �� � ���
        gameObject.SetActive(false);
    }
}
