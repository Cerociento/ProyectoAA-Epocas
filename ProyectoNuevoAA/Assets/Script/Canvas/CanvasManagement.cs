using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManagement : MonoBehaviour {

	//En Proceso

	[SerializeField]
	Text ammoFirstWeapon;
	[SerializeField]
	Text ammoSecondWeapon;
	GameObject player;
    GameObject parent;

    void Start ()
    {
		player = GameObject.Find("Player");
        parent = ammoFirstWeapon.gameObject.transform.parent.gameObject;
	}
	
	void Update ()
    {
        AmmoCount();
    }

	void AmmoCount()
    {
        #region Original
        /* float currentAmmoFirstWeapon; 
         float maxAmmoFirstWeapon;
         float currentAmmoSecondWeapon; 
         float maxAmmoSecondWeapon;
         currentAmmoFirstWeapon = player.GetComponent<WeaponsManager>().weaponsActive[1].GetComponent<BulletPool>().ammo;
         maxAmmoFirstWeapon = player.GetComponent<WeaponsManager>().weaponsActive[1].GetComponent<BulletPool>().maxAmmo;
         if(currentAmmoFirstWeapon>maxAmmoFirstWeapon)
             currentAmmoFirstWeapon=maxAmmoFirstWeapon;
         currentAmmoSecondWeapon = player.GetComponent<WeaponsManager>().weaponsActive[2].GetComponent<BulletPool>().ammo;
         maxAmmoSecondWeapon = player.GetComponent<WeaponsManager>().weaponsActive[2].GetComponent<BulletPool>().maxAmmo;
         if(currentAmmoSecondWeapon>maxAmmoSecondWeapon)
             currentAmmoSecondWeapon=maxAmmoSecondWeapon;
         ammoFirstWeapon.text= currentAmmoFirstWeapon+"/"+maxAmmoFirstWeapon;
         ammoSecondWeapon.text= currentAmmoSecondWeapon+"/"+maxAmmoSecondWeapon;*/
        #endregion

        float currentAmmoFirstWeapon;
        float maxAmmoFirstWeapon;

        if (player.GetComponent<WeaponsManager>().weaponsActive[1].activeInHierarchy)
        {
            parent.SetActive(true);
            currentAmmoFirstWeapon = player.GetComponent<WeaponsManager>().weaponsActive[1].GetComponent<BulletPool>().ammo;
            maxAmmoFirstWeapon = player.GetComponent<WeaponsManager>().weaponsActive[1].GetComponent<BulletPool>().maxAmmo;
            if (currentAmmoFirstWeapon > maxAmmoFirstWeapon)
                currentAmmoFirstWeapon = maxAmmoFirstWeapon;
            ammoFirstWeapon.text = currentAmmoFirstWeapon + "/" + maxAmmoFirstWeapon;
        }
        else if (player.GetComponent<WeaponsManager>().weaponsActive[2].activeInHierarchy)
        {
            parent.SetActive(true);
            currentAmmoFirstWeapon = player.GetComponent<WeaponsManager>().weaponsActive[2].GetComponent<BulletPool>().ammo;
            maxAmmoFirstWeapon = player.GetComponent<WeaponsManager>().weaponsActive[2].GetComponent<BulletPool>().maxAmmo;
            if (currentAmmoFirstWeapon > maxAmmoFirstWeapon)
                currentAmmoFirstWeapon = maxAmmoFirstWeapon;
            ammoFirstWeapon.text = currentAmmoFirstWeapon + "/" + maxAmmoFirstWeapon;
        }
        else
        {
            parent.SetActive(false);
        }
    }
}
