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

    //public void Shoot()
    //{
    //    if (ammoManager.CanShoot() && !isShooting && Time.time - lastShotTime >= timeBetweenShots)
    //    {
    //        isShooting = true;
    //        GameObject bullet = BulletPool.SharedInstance.GetPooledBullet();
    //        lastShotTime = Time.time;
    //        if (bullet != null)
    //        {
    //            bullet.transform.position = bulletSpawnPoint.position;
    //            bullet.transform.rotation = bulletSpawnPoint.rotation;
    //            bullet.SetActive(true);
    //            Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //            rb.velocity = bulletSpeed * bulletSpawnPoint.forward;
    //            ammoManager.UseAmmo();
    //            soundManager.PlayShootSound();
    //            animationManager.PlayShootAnimation();
    //            StartCoroutine(ResetShooting());
    //        }
    //    }
    //    else if (ammoManager.CanShoot() == false && !isShooting && !isEmptyMagazineSoundPlayed)
    //    {
    //        soundManager.PlayEmptyMagazineSound();
    //        Debug.Log("Magazine empty");
    //        isEmptyMagazineSoundPlayed = true;
    //    }
    //}

    public void Shoot()
    {
        if (ammoManager.CanShoot() && !isShooting && Time.time - lastShotTime >= timeBetweenShots)
        {
            isShooting = true;
            lastShotTime = Time.time;
            ScoreManager.Instance.RegisterShot();
            RaycastHit hit;
            if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit " + hit.transform.name); // Логирование попадания

                if (hit.collider.CompareTag("Target"))
                {
                    // Вызов метода для подсчета очков
                    hit.collider.GetComponent<TargetBehavior>()?.RegisterHit();
                }

                // Активация визуального объекта пули
                GameObject bullet = BulletPool.SharedInstance.GetPooledBullet();
                if (bullet != null)
                {
                    bullet.transform.position = bulletSpawnPoint.position;
                    bullet.transform.LookAt(hit.point); // Направляем пулю к месту попадания
                    bullet.SetActive(true);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    rb.velocity = bulletSpeed * bulletSpawnPoint.forward;
                    // Дополнительно можно добавить анимацию полета пули
                }
            }

            ammoManager.UseAmmo();
            soundManager.PlayShootSound();
            animationManager.PlayShootAnimation();

            StartCoroutine(ResetShooting());
        }
        else if (!ammoManager.CanShoot() && !isShooting)
        {
            if (!isEmptyMagazineSoundPlayed)
            {
                soundManager.PlayEmptyMagazineSound();
                isEmptyMagazineSoundPlayed = true;
            }
        }
    }

    private IEnumerator ResetShooting()
    {
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
        // Не сбрасываем isEmptyMagazineSoundPlayed здесь, чтобы звук мог воспроизводиться при каждой попытке стрельбы без патронов
    }

    public void ResetEmptyMagazineSoundPlayed()
    {
        isEmptyMagazineSoundPlayed = false;
    }
}