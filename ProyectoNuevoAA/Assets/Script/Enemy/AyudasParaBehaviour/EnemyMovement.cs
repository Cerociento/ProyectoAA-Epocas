using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;
	[SerializeField]
	Transform[] wayPoint;
	int nextPoint;
	public float wait=5f;


	// Use this for initialization
	void Start () {
		agent=GetComponent<UnityEngine.AI.NavMeshAgent>();
		NextWayPoint();
	}


	void NextWayPoint(){
		if(wayPoint.Length==0)
			return;
		agent.destination=wayPoint[nextPoint].position;
		nextPoint=(nextPoint+1)%wayPoint.Length;
	}

	void Update(){
		wait-=Time.deltaTime;
		if(wait<0f){
			NextWayPoint();
			wait=5f;
		}
	}

	void OnCollisionEnter(){
		
	}
}
