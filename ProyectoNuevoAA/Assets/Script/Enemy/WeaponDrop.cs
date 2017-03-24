using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDrop : MonoBehaviour {

	[SerializeField]
	GameObject enemyDrop;
	List<GameObject> enabledWeapons;

	// Use this for initialization
	void Start () {
		enabledWeapons=enemyDrop.GetComponent<FullListOfWeapons>().unlockedWeapons;
	}
	
	// Update is called once per frame
	void OnDisable () {
		//if(Input.GetKeyDown(KeyCode.P)){
			int prove;
			prove=Random.Range(0,enabledWeapons.Count);
			Debug.Log(enabledWeapons[prove].name);
		GameObject obj;
		obj=(GameObject)Instantiate(enabledWeapons[prove],transform.position,Quaternion.identity);
		obj.name=enabledWeapons[prove].name;
		}
	}
//}
