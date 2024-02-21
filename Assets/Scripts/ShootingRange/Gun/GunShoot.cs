using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GunShoot : MonoBehaviour
{
    [Header("Shooting Parameters")]
    public Transform bulletSpawnPoint; // точка, откуда вылетает пуля
    public float bulletSpeed = 1000f; // скорость пули

    [Header("Ammo Indicators")]
    public GameObject[] bulletIndicators; // индикаторы патронов
    public int maxAmmo = 6; // максимальное количество патронов
    private int currentAmmo; // текущее количество патронов

    [Header("Magazine Indicators")]
    public GameObject[] magazineIndicators; // индикаторы магазинов
    private int totalMagazines = 5; // всего магазинов, включая актуальный

    [Header("Sound Effects")]
    public AudioClip shootSound; // звук выстрела
    private AudioSource audioSource; // компонент для воспроизведения звуков
    public AudioSource boltSound; // звук затвора
    public AudioClip emptyMagazineSound; // звук пустого магазина
    public AudioClip reloadSound; // звук перезарядки

    [Header("Animation")]
    public Animator gunAnimator; // аниматор для анимаций оружия
    private string shootTriggerName = "Shoot"; // имя триггера для анимации выстрела

    private bool isShooting = false; // флаг, что сейчас идет стрельба
    private bool isReloading = false; // флаг, что сейчас идет перезарядка

    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    private InputDeviceCharacteristics rightHandCharacteristics =
    InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

    private float longPressDuration = 2.0f; // время, считающееся длительным нажатием
    private bool isEmptyMagazineSoundPlayed = false; // флаг воспроизведения звука пустого магазина

    private bool isTriggerDown = false;
    private float triggerDownTime = 0f;

    void Awake()
    {
        /*  
        RU: инициализирую компонент AudioSource
        EN: initializing the AudioSource component
        */
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        /*  
        RU: получаю устройства ввода для правой руки и инициализирую патроны
        EN: getting input devices for the right hand and initializing ammo
        */
        InputDevices.GetDevicesWithCharacteristics(rightHandCharacteristics, rightHandDevices);
        currentAmmo = maxAmmo;
        UpdateBulletIndicators();
    }

    void Update()
    {
        /*  
        RU: проверяю нажатие и отпускание триггера для стрельбы или перезарядки
        EN: checking for trigger press and release for shooting or reloading
        */
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.1f && !isTriggerDown)
            {
                isTriggerDown = true;
                triggerDownTime = Time.time;
                isEmptyMagazineSoundPlayed = false;
            }
            else if (triggerValue < 0.1f && isTriggerDown)
            {
                if (Time.time - triggerDownTime >= longPressDuration)
                {
                    TryReload();
                }
                else
                {
                    Shoot();
                }
                isTriggerDown = false;
            }
        }
    }

    public void Shoot()
    {
        /*  
        RU: выполняю выстрел, если есть патроны, иначе воспроизвожу звук пустого магазина
        EN: performing a shot if ammo is available, otherwise playing the empty magazine sound
        */
        if (currentAmmo > 0 && !isShooting)
        {
            isShooting = true;
            GameObject bullet = BulletPool.SharedInstance.GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetActive(true);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bulletSpeed * bulletSpawnPoint.forward;
                currentAmmo--;
                UpdateBulletIndicators();
                audioSource.PlayOneShot(shootSound);
                gunAnimator.SetTrigger(shootTriggerName);
                boltSound.Play();
                StartCoroutine(ResetShooting());
                isEmptyMagazineSoundPlayed = false;
                ScoreManager.Instance.RegisterShot();
            }
        }
        else if (currentAmmo == 0 && !isShooting && !isEmptyMagazineSoundPlayed)
        {
            audioSource.PlayOneShot(emptyMagazineSound);
            Debug.Log("Magazine empty");
            isEmptyMagazineSoundPlayed = true;
        }
    }

    private IEnumerator ResetShooting()
    {
        /*  
        RU: ожидаю для сброса состояния стрельбы
        EN: waiting to reset the shooting state
        */
        yield return new WaitForSeconds(1.0f);
        isShooting = false;
    }

    void UpdateBulletIndicators()
    {
        /*  
        RU: обновляю индикаторы патронов
        EN: updating the ammo indicators
        */
        for (int i = 0; i < bulletIndicators.Length; i++)
        {
            bulletIndicators[i].SetActive(i < currentAmmo);
        }
    }

    void UpdateMagazineIndicators()
    {
        /*  
        RU: обновляю индикаторы магазинов
        EN: updating the magazine indicators
        */
        for (int i = 1; i < magazineIndicators.Length; i++)
        {
            magazineIndicators[i].SetActive(i < totalMagazines);
        }
    }

    public void TryReload()
    {
        if (!isReloading && currentAmmo < maxAmmo && totalMagazines > 1) // проверяем наличие запасных магазинов
        {
            Debug.Log("Reloading initiated");
            audioSource.PlayOneShot(reloadSound); // воспроизводим звук перезарядки
            StartCoroutine(Reload());
        }
        else if (totalMagazines <= 1) // если это последний магазин или магазинов нет
        {
            Debug.Log("No magazines left to reload.");
        }
        else
        {
            Debug.Log("Reload not required or already reloading");
        }
    }


    private IEnumerator Reload()
    {
        /*  
        RU: процесс перезарядки с имитацией задержки
        EN: the reloading process with a simulated delay
        */
        isReloading = true;
        Debug.Log("Started reload coroutine");

        // Имитируем задержку для перезарядки
        yield return new WaitForSeconds(2.0f);

        if (totalMagazines > 1)
        {
            totalMagazines--; // уменьшаем количество магазинов на 1
            currentAmmo = maxAmmo; // перезаряжаем магазин
            UpdateBulletIndicators(); // обновляем индикаторы патронов
            UpdateMagazineIndicators(); // обновляем индикаторы магазинов
            Debug.Log($"Reloaded. Current ammo: {currentAmmo}. Magazines left: {totalMagazines - 1}");
        }
        else
        {
            Debug.Log("No magazines left to reload.");
        }

        isReloading = false;
    }
}