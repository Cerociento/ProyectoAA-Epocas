using UnityEngine;
using System.Collections.Generic;

public class WeaponsManager : MonoBehaviour {

    [SerializeField]
	public List<GameObject> weaponsActive = new List<GameObject>(3);
    [SerializeField]
    //AQUI van todas las armas del juego
    List<GameObject> weapons;
	[Tooltip("Cantidad de munición que se restaurará al coger un arma repetida. No sobrepasará el máximo del arma.")]
	[SerializeField]
	int ammoRestore;
     int weaponsBackpack = 0;
    float scroll;
    [SerializeField]
    int weaponNumber;
	public int weaponAmmo;

    void Update ()
    {
        SelectWeapond();
    }

    void SelectWeapond()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            weaponsBackpack++;
        }
        else if (scroll < 0)
        {
            weaponsBackpack--;
        }

        switch (weaponsBackpack)
        {
            case 0:
                weaponsActive[0].SetActive(true);
                weaponsActive[1].SetActive(false);
                weaponsActive[2].SetActive(false);
                break;

            case 1:
                weaponsActive[1].SetActive(true);
                weaponsActive[2].SetActive(false);
                weaponsActive[0].SetActive(false);
                break;

            case 2:
                weaponsActive[2].SetActive(true);
                weaponsActive[0].SetActive(false);
                weaponsActive[1].SetActive(false);
                break;
        }

        if (weaponsBackpack <= -1)
            weaponsBackpack = 2;
        else if (weaponsBackpack >= 3)
            weaponsBackpack = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            //int numberRandom = Random.Range(1, 3); 
            weaponNumber = other.GetComponent<BulletPool>().AsWeaponActive;
            if (weaponsActive.IndexOf(weapons[weaponNumber]) < 0  && Input.GetKeyDown(KeyCode.E) && weaponsBackpack != 0)
            {
                #region antiguo
                /*weaponsActive[numberRandom].SetActive(false);
                weaponsActive[numberRandom] = weapons[weaponNumber];*/
                /*Si queres que se active automaticamente el arma NUEVA
                Descomentar la linea de abajo*/
                //weaponsBackpack = numberRandom;
                #endregion

                weaponsActive[weaponsBackpack].SetActive(false);
                weaponsActive[weaponsBackpack] = weapons[weaponNumber];

                if (!other.GetComponent<BulletPool>().ammoBox)
                    other.gameObject.SetActive(false);
                Debug.Log("NUEVA  " + weaponsActive[weaponsBackpack].name);
            }
            else
            {
                foreach(GameObject reloadAmmo in weaponsActive)
                {
                    if(weaponNumber == reloadAmmo.GetComponent<BulletPool>().AsWeaponActive)
                    {
                        reloadAmmo.GetComponent<BulletPool>().ammo += ammoRestore;
                        Debug.Log("Recarga  " + reloadAmmo.name);
                        if (!other.GetComponent<BulletPool>().ammoBox)
                            other.gameObject.SetActive(false);
                    }
                }
				//weaponsActive[weaponsBackpack].GetComponent<BulletPool>().ammo += ammoRestore;
            }
        }        
    }
}
