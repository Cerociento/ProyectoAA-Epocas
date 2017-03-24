using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {


	public float timer;
	[SerializeField]
	public int newTarget;
	public float speed;
	public NavMeshAgent agent;
	public Vector3 Target;
	[SerializeField]
	float range;

	// Use this for initialization
	void Start () {
		agent=GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyMoves();
		}
		//agent.speed=speed;



	void NextTarget(){
		float xEnemy=gameObject.transform.position.x;
		float zEnemy=gameObject.transform.position.z;

		float xPos=Random.Range(xEnemy-range, xEnemy+range);
		float zPos=Random.Range(zEnemy-range, zEnemy+range);

		Target=new Vector3(xPos,gameObject.transform.position.y,zPos);

		agent.SetDestination(Target);
	}

	void EnemyMoves(){
		timer+=Time.deltaTime;
		if(timer>=newTarget){
			NextTarget();
			timer=0;
		}
	}
}
