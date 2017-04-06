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

	// Use this for initialization
	void Start () {
		player=GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		AmmoCount();
	}


		void AmmoCount(){
		float currentAmmoFirstWeapon; 
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
		ammoSecondWeapon.text= currentAmmoSecondWeapon+"/"+maxAmmoSecondWeapon;
	}
}
