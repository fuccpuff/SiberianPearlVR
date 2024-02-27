using System.Collections;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [Header("Ammo Indicators")]
    public GameObject[] bulletIndicators;
    public int maxAmmo = 6;
    private int currentAmmo;

    [Header("Magazine Indicators")]
    public GameObject[] magazineIndicators;
    private int totalMagazines = 5;

    private SoundManager soundManager;

    public GunShoot gunShoot; // Добавьте это поле через Inspector Unity

    void Awake()
    {
        soundManager = GetComponent<SoundManager>();
    }

    public void Initialize()
    {
        currentAmmo = maxAmmo;
        UpdateBulletIndicators();
        UpdateMagazineIndicators();
    }

    public bool CanShoot()
    {
        return currentAmmo > 0;
    }

    public void UseAmmo()
    {
        currentAmmo--;
        UpdateBulletIndicators();
    }

    public void TryReload()
    {
        if (currentAmmo < maxAmmo && totalMagazines > 0)
        {
            StartCoroutine(Reload());
        }
        else if (totalMagazines <= 0)
        {
            Debug.Log("Нет магазинов для перезарядки.");
        }
        else
        {
            Debug.Log("Перезарядка не требуется или уже идет.");
        }
    }

    private IEnumerator Reload()
    {
        soundManager.PlayReloadSound();
        Debug.Log("Начата перезарядка");

        yield return new WaitForSeconds(2.0f);

        if (totalMagazines > 0)
        {
            totalMagazines--;
            currentAmmo = maxAmmo;
            UpdateBulletIndicators();
            UpdateMagazineIndicators();
            Debug.Log($"Перезаряжено. Текущее количество патронов: {currentAmmo}. Осталось магазинов: {totalMagazines}.");

            // Вызываем метод сброса в GunShoot
            if (gunShoot != null)
            {
                gunShoot.ResetEmptyMagazineSoundPlayed();
            }
        }
        else
        {
            Debug.Log("Магазины закончились.");
        }
    }


    private void UpdateBulletIndicators()
    {
        for (int i = 0; i < bulletIndicators.Length; i++)
        {
            bulletIndicators[i].SetActive(i < currentAmmo);
        }
    }

    private void UpdateMagazineIndicators()
    {
        for (int i = 0; i < magazineIndicators.Length; i++)
        {
            // Первый индикатор всегда активен, так как он показывает текущий магазин
            magazineIndicators[i].SetActive(i < totalMagazines - 1);
        }
    }
}
