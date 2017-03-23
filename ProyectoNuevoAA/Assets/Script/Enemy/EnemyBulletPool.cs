using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBulletPool : MonoBehaviour {

	List<GameObject> pool = new List<GameObject>();
	[SerializeField]
	GameObject bullet;
	[SerializeField]
	GameObject canon;
	[SerializeField]
	GameObject gunMagazine;
	int maxBullets = 10;
	[SerializeField]
	float fireRate=1f;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < maxBullets; i++) {
			GameObject obj=(GameObject)Instantiate(bullet,canon.transform.position,Quaternion.identity, gunMagazine.transform);
			obj.SetActive(false);
			pool.Add(obj);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
		Attack();

	}

	void Fire(){
		for (int i = 0; i < pool.Count; i++) {
			if(!pool[i].activeInHierarchy){
				pool[i].transform.position=transform.position;
				pool[i].transform.rotation=transform.rotation;
				pool[i].SetActive(true);
				break;
			}
		}
	}

	void Attack(){
		fireRate-=Time.deltaTime;
		if(fireRate<0f){
			Fire();
			fireRate=1f;
		}
	}
		
}
