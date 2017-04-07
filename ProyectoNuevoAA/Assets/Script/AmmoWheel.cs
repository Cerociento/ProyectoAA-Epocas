using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoWheel : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject weaponWheel;
    Animator anim;
    int weapon;
    float weaponActive;
    [SerializeField]
    Image active, up, down;
    Sprite one, two, three;

    void Start ()
    {
        anim = weaponWheel.GetComponent<Animator>();
    }

    public void Update()
    {
        weapon = player.GetComponent<WeaponsManager>().weaponsBackpack;
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            anim.SetTrigger("Change");

    }
    public void ChangeImage()
    {
        
        one = player.GetComponent<WeaponsManager>().weaponsActive[0].GetComponent<BulletPool>().imageWeapon;
        two = player.GetComponent<WeaponsManager>().weaponsActive[1].GetComponent<BulletPool>().imageWeapon;
        three = player.GetComponent<WeaponsManager>().weaponsActive[2].GetComponent<BulletPool>().imageWeapon;

        switch (weapon)
        {
            case 0:
                active.sprite = one;
                up.sprite = two;
                down.sprite = three;
                break;
            case 1:
                active.sprite = two;
                up.sprite = three;
                down.sprite = one;
                break;
            case 2:
                active.sprite = three;
                up.sprite = one;
                down.sprite = two;
                break;
        }
    }
}
