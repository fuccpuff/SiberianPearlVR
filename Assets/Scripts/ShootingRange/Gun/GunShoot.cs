using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GunShoot : MonoBehaviour
{
    [Header("Shooting Parameters")]
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 1000f;

    private AmmoManager ammoManager;
    private SoundManager soundManager;
    private AnimationManager animationManager;

    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    private InputDeviceCharacteristics rightHandCharacteristics =
        InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

    private bool isTriggerDown = false;
    private float triggerDownTime = 0f;
    private float longPressDuration = 2.0f;

    private bool isShooting = false;
    private float timeBetweenShots = 1f;
    private float lastShotTime = -1f;
    private bool isEmptyMagazineSoundPlayed = false;

    void Awake()
    {
        ammoManager = GetComponent<AmmoManager>();
        soundManager = GetComponent<SoundManager>();
        animationManager = GetComponent<AnimationManager>();
    }

    void Start()
    {
        InputDevices.GetDevicesWithCharacteristics(rightHandCharacteristics, rightHandDevices);
        ammoManager.Initialize();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            HandleTriggerInput(triggerValue);
        }
    }

    private void HandleTriggerInput(float triggerValue)
    {
        if (triggerValue > 0.1f && !isTriggerDown)
        {
            isTriggerDown = true;
            triggerDownTime = Time.time;
        }
        else if (triggerValue < 0.1f && isTriggerDown)
        {
            if (Time.time - triggerDownTime >= longPressDuration)
            {
                ammoManager.TryReload();
            }
            else
            {
                Shoot();
            }
            isTriggerDown = false;
        }
    }

    public void Shoot()
    {
        if (ammoManager.CanShoot() && !isShooting && Time.time - lastShotTime >= timeBetweenShots)
        {
            isShooting = true;
            GameObject bullet = BulletPool.SharedInstance.GetPooledBullet();
            lastShotTime = Time.time;
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetActive(true);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bulletSpeed * bulletSpawnPoint.forward;
                ammoManager.UseAmmo();
                soundManager.PlayShootSound();
                animationManager.PlayShootAnimation();
                StartCoroutine(ResetShooting());
            }
        }
        else if (!ammoManager.CanShoot() && !isShooting)
        {
            soundManager.PlayEmptyMagazineSound();
            // Сразу после воспроизведения звука сбрасываем флаг, чтобы обеспечить его воспроизведение при следующей попытке.
            isEmptyMagazineSoundPlayed = false;
        }
    }


    private IEnumerator ResetShooting()
    {
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
        if (ammoManager.CanShoot() == false)
        {
            // Это изменение позволяет повторно воспроизводить звук пустого магазина,
            // если игрок пытается стрелять без патронов.
            isEmptyMagazineSoundPlayed = false;
        }
    }

    public void ResetEmptyMagazineSoundPlayed()
    {
        isEmptyMagazineSoundPlayed = false;
    }
}