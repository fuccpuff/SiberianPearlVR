using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;
    public List<GameObject> pooledBullets;
    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    public int maxBullets = 100; // Максимальное количество пуль в пуле

    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем пул пуль между сценами
        }
        else
        {
            Destroy(gameObject);
        }

        pooledBullets = new List<GameObject>();
        InitializePool();
    }

    // Инициализация пула с предварительным "прогревом"
    void InitializePool()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            AddBulletToPool();
        }
    }

    GameObject AddBulletToPool()
    {
        if (pooledBullets.Count >= maxBullets) return null; // Проверяем, не превышен ли максимальный лимит

        var newBullet = Instantiate(bulletPrefab, transform);
        newBullet.SetActive(false);
        pooledBullets.Add(newBullet);
        return newBullet;
    }

    public GameObject GetPooledBullet()
    {
        foreach (var bullet in pooledBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        // Расширяем пул, если достигнут максимальный предел
        return pooledBullets.Count < maxBullets ? AddBulletToPool() : null;
    }
}