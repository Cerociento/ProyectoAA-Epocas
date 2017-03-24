using UnityEngine;
using System.Collections.Generic;

public class WeaponsManagerII : MonoBehaviour {

    [SerializeField]
    List<GameObject> weaponsActive; // = new GameObject[3];
    [SerializeField]
    List<GameObject> weapons;
    int weaponsBackpack = 1;
    float scroll;

	void Start(){
		weaponsActive.Add(weapons[0]);
	}

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
            case 1:
                weaponsActive[0].SetActive(true);
                weaponsActive[1].SetActive(false);
                weaponsActive[2].SetActive(false);
                break;

            case 2:
                weaponsActive[1].SetActive(true);
                weaponsActive[2].SetActive(false);
                weaponsActive[0].SetActive(false);
                break;

            case 3:
                weaponsActive[2].SetActive(true);
                weaponsActive[0].SetActive(false);
                weaponsActive[1].SetActive(false);
                break;
        }

        if (weaponsBackpack <= 0)
            weaponsBackpack = 1;
        else if (weaponsBackpack >= 4)
            weaponsBackpack = 3;
    }


	/*
		Bien. En esta parte, el jugador pasa por algún arma que se haya dropeado. Compara su nombre, y si no la tiene y tiene espacio, la recoge.
		Si no tiene espacio, sustituye un arma que tenga siempre que NO sea la pistola. Si tiene tres armas y el arma que ha caído ya la tiene, 
		no la recoge (hay que poner sistema de munición).
	*/
	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Weapon")){
			string weaponName;
			weaponName=col.name;
			int weaponNumber=0;
			switch (weaponName) {
			case "MachineGun":
				weaponNumber=1;
				break;
			case "Gunshot":
				weaponNumber=2;
				break;
			}

			if(weaponsActive.Contains(weapons[weaponNumber]))
				return;
			else if(weaponsActive.Count==3 && weaponsBackpack==1)
				return;
			else if(weaponsActive.Count==3 && weaponsBackpack!=1){
				weaponsActive[weaponsBackpack-1].SetActive(false);
				weaponsActive.Remove(weaponsActive[weaponsBackpack-1]);
				weaponsActive.Add(weapons[weaponNumber]);
			} else{
				weaponsActive.Add(weapons[weaponNumber]);
				weaponsBackpack++;
				weaponsActive[0].SetActive(false);
				//weaponsActive[weaponsBackpack]
			}
			col.gameObject.SetActive(false);
		}
    }
}
