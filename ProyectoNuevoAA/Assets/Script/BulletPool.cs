﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform barrel;
    List<GameObject> bullets = new List<GameObject>();
    bool expandable = false;
    [SerializeField]
    byte maxAmount = 5;
    [SerializeField]
    byte minAmount = 3;
    [SerializeField]
    float shotDelay = 4f;
    [SerializeField]
    int weaponClass = 0;
    [SerializeField]
    Transform gunMagazine;


    public GameObject GetBullet()
    {
        byte index = 0;

        foreach (GameObject bullet in bullets)
        {
            if (!expandable && index >= maxAmount)
                break;

            if(!bullet.activeSelf)
            {
                bullet.transform.position = barrel.position;
                bullet.transform.rotation = barrel.rotation;
                bullet.SetActive(true);
                expandable = false;
                return bullet;
            }

            if(bullet.activeSelf && bullets.Count >=maxAmount)
            {
                expandable = true;
            }
            index++;
        }

        if(!expandable && bullets.Count >= maxAmount)
        {
            return null;
        }

        GameObject newBullet = InstantiateBullet();
        newBullet.SetActive(true);
        return InstantiateBullet();
    }

    GameObject InstantiateBullet()
    {
		GameObject obj = Instantiate(bulletPrefab, barrel.position, Quaternion.identity,gunMagazine) as GameObject;
        bullets.Add(obj);
        return obj;
    }

    void Start()
    {
        for (byte i = 0; i < minAmount; i++)
        {
            InstantiateBullet();
        }
    }

    void Update()
    {
#region Editor
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponClass = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotDelay = 4;
            weaponClass = 1;
            bullets.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotDelay = 0.5f;
            weaponClass = 2;
            bullets.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponClass = 3;
            bullets.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponClass = 4;
            bullets.Clear();
        }
#endif
#endregion
        Shoot();
    }

    void Shoot()
    {
        /*
         0 = Gun // Delay 0.3 
         1 = Machine Gun // Delay = 4
         2 = Shotgun // Delay = 0.5
          */

        if (weaponClass == 0|| weaponClass == 3)
        {
            if (shotDelay > 0)
                shotDelay -= Time.deltaTime;
            else if (shotDelay <= 0)
            {
                shotDelay = 0;
                if (Input.GetButtonDown("Fire1"))
                {
                    GetBullet();
                    shotDelay = 0.3f;
                }
            }
        }
        else if (weaponClass == 1 && Input.GetButton("Fire1"))
        {
            shotDelay--;
            if (shotDelay <= 0)
            {
                GetBullet();
                shotDelay = 4;
            }
        }
        else if (weaponClass == 2)
        {
            if (shotDelay > 0)
                shotDelay -= Time.deltaTime;
            else if (shotDelay <= 0)
            {
                shotDelay = 0;
                if (Input.GetButtonDown("Fire1"))
                {
                    GetBullet();
                    shotDelay = 0.5f;
                }
            }
        }
    }
}
