using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomWeapon : MonoBehaviour {

	[SerializeField]
	GameObject[] weapons;
	[SerializeField]
	List<GameObject> unlockedWeapons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			if(unlockedWeapons.Contains(weapons[2]))
				return;
			else
			unlockedWeapons.Add(weapons[2]);
		}

		if(Input.GetKeyDown(KeyCode.Alpha9)){
			int randomWeapon=Random.Range(0,weapons.Length);
			if(unlockedWeapons.Contains(weapons[randomWeapon]))
				return;
				else
			unlockedWeapons.Add(weapons[randomWeapon]);
		}
	}


}

/*
void OnTriggerEnter(Collider col){
		if(col.CompareTag){
			int randomWeapon=Random.Range(0,weapons.Count);
			if(weaponsActive.Contains(weapons[randomWeapon]))
				return;
			else if(weaponsActive.Count==3 && weaponsBackpack==1)
				return;
			else if(weaponsActive.Count==3 && weaponsBackpack!=1){
				weaponsActive[weaponsBackpack-1].SetActive(false);
				weaponsActive.Remove(weaponsActive[weaponsBackpack-1]);
				weaponsActive.Add(weapons[randomWeapon]);
			} else{
				weaponsActive.Add(weapons[randomWeapon]);
				weaponsBackpack++;
				weaponsActive[0].SetActive(false);
				//weaponsActive[weaponsBackpack]
			}
		}
*/
