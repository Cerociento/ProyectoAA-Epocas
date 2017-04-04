﻿using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	[SerializeField]
	int minAmountOfEnemies;
	[SerializeField]
	int maxAmountOfEnemies;
	[SerializeField]
	GameObject[] typeOfEnemies;
	[SerializeField]
	Transform[] spawnPoint;
    [Tooltip("Activar para que pare la cámara al pasar")]
    [SerializeField]
    bool camInactive;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player"))
        {
          if(camInactive)
            Camera.main.GetComponent<CameraMov>().active = false;
        
		int amountOfEnemies=Random.Range(minAmountOfEnemies, maxAmountOfEnemies);
		int enemyType;
          foreach (Transform point in spawnPoint)
            {
              for (int i = 0; i < amountOfEnemies; i++)
                {
                   enemyType = Random.Range(0, typeOfEnemies.Length);
                   Instantiate(typeOfEnemies[enemyType], point.position, Quaternion.identity);
                }
            }

		gameObject.SetActive(false);
	}
	}
}
