using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	float health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Bullet")){
			health--;
		}
		if(health==0){
			gameObject.SetActive(false);
		}
	}
}
