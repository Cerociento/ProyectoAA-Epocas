using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FullListOfWeapons : MonoBehaviour {

	[SerializeField]
	List<GameObject> allWeapons;
	public List<GameObject> unlockedWeapons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Esto es solo una prueba para comprobar que si ya estaba ese arma desbloqueada, no se volviese a desbloquear
		/*
		if(Input.GetKeyDown(KeyCode.M)){
			for (int i = 1; i < allWeapons.Count; i++) {
				if (!unlockedWeapons.Contains(allWeapons[i])){
					unlockedWeapons.Add(allWeapons[i]);
					break;}

		}
	}
	*/
	}
}
