using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {

	private delegate void UpdateState();
	private UpdateState updateCurrentState;

	enum States{Move, Shoot}
	States currentState=States.Move;

	//MIRADA//
	//[SerializeField]
	GameObject player;

	//MOVIMIENTO//
	public float timer;
	public NavMeshAgent agent;
	public Vector3 Target;
	[SerializeField]
	float range;
	[SerializeField]
	float initialWait=0f;
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
	[SerializeField] [Tooltip("Tiempo inicial antes de dar una cuchillada")]
	float stabWaitTime=2f;
	[SerializeField] [Tooltip("Tiempo entre cuchilladas")]
	float stabRate=4f;
	[SerializeField] [Tooltip("Distancia de cuchillada")]
	float stabDistance=3f;
	float rate;
	public Vector3 startingPosition;
	[Tooltip("Primer movimiento aleatorio")]
	public bool firstMove=true;

	// Use this for initialization
	void Start () {

		startingPosition=gameObject.transform.position;
		player=GameObject.Find("Player");
		agent=GetComponent<NavMeshAgent>();
		updateCurrentState+=EnemyMoves;
		for (int i = 0; i < maxBullets; i++) {
			GameObject obj=(GameObject)Instantiate(bullet,canon.transform.position,Quaternion.identity);// gunMagazine.transform);
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
		NextTarget();
		updateCurrentState+=EnemyMoves;
	}

	void ToFire(){
		
		//Debug.Log("Estoy aquí");
		if(currentState==States.Shoot){
			return;
		} else if (currentState==States.Move){
			updateCurrentState-=EnemyMoves;
		}
		currentState=States.Shoot;
		updateCurrentState+=Attack;
	}


	//MOVIMIENTO//
	/*void NextWayPoint(){
		if(wayPoint.Length==0)
			return;
		agent.destination=wayPoint[nextPoint].position;
		nextPoint=(nextPoint+1)%wayPoint.Length;
		}*/

	void NextTarget(){
        Debug.Log("w");
		float xEnemy=gameObject.transform.position.x;
		float zEnemy=gameObject.transform.position.z;

		if(!firstMove){
		xEnemy=player.transform.position.x;
		zEnemy=player.transform.position.z;
			startingPosition=player.transform.position;}
		if(firstMove){
		xEnemy=gameObject.transform.position.x;
		zEnemy=gameObject.transform.position.z;
			firstMove=false;
		}

		float xPos=Random.Range(xEnemy-range, xEnemy+range);
		float zPos=Random.Range(zEnemy-range/2, zEnemy+range/2);


		if(xPos>startingPosition.x+range)
			xPos=startingPosition.x+range;
		else if(xPos<startingPosition.x-range)
			xPos=startingPosition.x-range;
		else if(zPos>startingPosition.z+range)
			zPos=startingPosition.z+range;
		else if(zPos<startingPosition.z-range)
			zPos=startingPosition.z-range;
		Target=new Vector3(xPos,gameObject.transform.position.y,zPos);

		agent.SetDestination(Target);

	}



	void EnemyMoves(){
		/*timer+=Time.deltaTime;
		if(timer>=newTarget){
			NextTarget();
			timer=0;
		}*/
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
		if(Vector3.Distance(gameObject.transform.position,player.transform.position)>stabDistance){
		rate-=Time.deltaTime;
		if(rate<0f){
			Debug.Log("Pew pew");
			Fire();
			rate+=fireRate;
		}
		}else{
			stabWaitTime-=Time.deltaTime;
			if(stabWaitTime<0){
				Debug.Log("Cuchillada");
				player.GetComponent<Health>().health--;
				player.GetComponent<Health>().noEnemyDamage = true;
				stabWaitTime+=stabRate;
			}
		}
		initialWait-=Time.deltaTime;
		if(initialWait<0f){
		initialWait+=timeToWait;
			ToMove();

		}
	}
}
