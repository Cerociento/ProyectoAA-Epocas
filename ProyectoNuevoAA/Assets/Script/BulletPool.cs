using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {

    GameObject bulletPrefab;
    [SerializeField]
    GameObject[] amount;
    [SerializeField]
    Transform barrel;
    List<GameObject> bullets = new List<GameObject>();
    bool expandable = false;
    [SerializeField]
    byte maxAmount = 5;
    [SerializeField]
    byte minAmount = 3;
    int amountClass = 0;
    float count = 4f;

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
        GameObject obj = Instantiate(bulletPrefab, barrel.position, Quaternion.identity) as GameObject;
        bullets.Add(obj);
        return obj;
    }

    void Awake()
    {
        bulletPrefab = amount[0];
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

        bulletPrefab = amount[amountClass];
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            amountClass = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            count = 4;
            amountClass = 1;
            bullets.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            count = 0.5f;
            amountClass = 2;
            bullets.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            amountClass = 3;
            bullets.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            amountClass = 4;
            bullets.Clear();
        }
#endif
        if (amountClass == 0 && Input.GetButtonDown("Fire1")|| amountClass == 3 && Input.GetButtonDown("Fire1"))
        {
            GetBullet();
        }
        else if (amountClass == 1 && Input.GetButton("Fire1"))
        {
            count--;
            if (count <= 0)
            {
                GetBullet();
                count = 4;
            }
        }
        else if (amountClass == 2)
        {
            if (count >= 0)
                count -= Time.deltaTime;
            else if (count <= 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    GetBullet();
                    count = 0.5f;
                }
            }
        }
        else if (amountClass == 4)
        {
            amount[4].SetActive(true);
        }
    }
}
