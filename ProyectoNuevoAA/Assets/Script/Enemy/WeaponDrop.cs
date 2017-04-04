using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDrop : MonoBehaviour {

	[SerializeField]
	//Aquí se pone AllWeapons. De aquí se llega a la lista en la que están todas las armas
	GameObject enemyDrop;
	[SerializeField]
	GameObject medipack;
	[SerializeField]
	int probability;
	[SerializeField]
	int dropWeaponRate;
	List<GameObject> enabledWeapons;

	// Use this for initialization
	void Start ()
    {
		//Accedemos a la lista de armas desbloqueadas.
		enabledWeapons=enemyDrop.GetComponent<FullListOfWeapons>().unlockedWeapons;
	}
	
	// Update is called once per frame
	public void Drop() {
		//if(Input.GetKeyDown(KeyCode.P)){
		/*Esto hace que, al matar a un enemigo, se tire una probabilidad. Si ese número que sale es menor a la probabilidad de drop,
		vuelve a generar un número aleatorio. Si ese número es menor o igual a la probabilidad de drop del arma, sale un arma aleatoria.
		En otro caso, saldrá un botiquín que restaure vida.
		Siempre dará un arma que esté desbloqueada, nunca una que no lo esté.
		*/
		int dropChance;
		dropChance=Random.Range(0,100);
		if(dropChance<=probability){
			int weaponOrHealth;
			weaponOrHealth=Random.Range(0,100);
			if(weaponOrHealth<=dropWeaponRate){
				int order;
				order=Random.Range(0,enabledWeapons.Count);
				GameObject obj;
				obj=(GameObject)Instantiate(enabledWeapons[order],transform.position,Quaternion.identity);
				obj.name=enabledWeapons[order].name;
			}else{
				Instantiate(medipack,transform.position,Quaternion.identity);
			}
		  }
	   }
	}
//}
