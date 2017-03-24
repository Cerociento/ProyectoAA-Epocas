using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {

	private delegate void UpdateState();
	private UpdateState updateCurrentState;

	enum States{Move, Shoot}
	States currentState=States.Shoot;

	//MIRADA//
	[SerializeField]
	GameObject player;

	//MOVIMIENTO//

	NavMeshAgent agent;
	[SerializeField]
	Transform[] wayPoint;
	int nextPoint;
	public float wait=5f;
	[SerializeField] 
	float timeToWait;


	//DISPARO//
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
		
		agent=GetComponent<NavMeshAgent>();
		updateCurrentState+=Attack;
		for (int i = 0; i < maxBullets; i++) {
			GameObject obj=(GameObject)Instantiate(bullet,canon.transform.position,Quaternion.identity, gunMagazine.transform);
			obj.SetActive(false);
			pool.Add(obj);

		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			ToMove();
		} else if(Input.GetKeyDown(KeyCode.Alpha2)){
			ToFire();
		}
		updateCurrentState();
	}

	void ToMove(){
		
		if(currentState==States.Move){
			return;
		} else if (currentState==States.Shoot){
			updateCurrentState-=Attack;
		}
		currentState=States.Move;
		agent.enabled=true;
		NextWayPoint();
		updateCurrentState+=EnemyMoves;
	}

	void ToFire()
    {
		if(currentState==States.Shoot){
			return;
		} else if (currentState==States.Move){
			updateCurrentState-=EnemyMoves;
		}
		currentState=States.Shoot;
		updateCurrentState+=Attack;
	}


	//MOVIMIENTO//
	void NextWayPoint(){
		if(wayPoint.Length==0)
			return;
		agent.destination=wayPoint[nextPoint].position;
		nextPoint=(nextPoint+1)%wayPoint.Length;
		}




	void EnemyMoves(){
		if(agent.remainingDistance<0.1f){
			//transform.LookAt(player.transform.position);
			ToFire();
		}
	}


	//DISPARO//
	void Fire(){
		
		for (int i = 0; i < pool.Count; i++) {
			if(!pool[i].activeInHierarchy){
				pool[i].transform.position=canon.transform.position;
				pool[i].transform.rotation=canon.transform.rotation;
				pool[i].SetActive(true);
				//Debug.Log("Bala va");
				break;
			}
		}
	}

	void Attack(){
		//Debug.Log("Attack");
		transform.LookAt(player.transform.position);
		agent.enabled=false;
		fireRate-=Time.deltaTime;
		if(fireRate<0f){
			//Debug.Log("Pew pew");
			Fire();
			fireRate=1f;
		}
		wait-=Time.deltaTime;
		if(wait<0f){
		wait+=timeToWait;
			ToMove();

		}
	}
}
