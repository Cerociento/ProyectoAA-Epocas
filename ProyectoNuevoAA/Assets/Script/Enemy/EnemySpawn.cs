using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	[SerializeField]
	int minAmountOfEnemies;
	[SerializeField]
	int maxAmountOfEnemies;
	[SerializeField]
	GameObject[] typeOfEnemies;
	[SerializeField]
	Transform spawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
		int amountOfEnemies=Random.Range(minAmountOfEnemies, maxAmountOfEnemies);
		int enemyType;
		for (int i = 0; i < amountOfEnemies; i++) {
			enemyType=Random.Range(0,typeOfEnemies.Length);
				Instantiate(typeOfEnemies[enemyType], spawnPoint.position, Quaternion.identity);
		}
		gameObject.SetActive(false);
	}
	}
}
