using UnityEngine;
using System.Collections;

public class EnemyBulletForce : MonoBehaviour {

	Rigidbody rigid;
	[SerializeField]
	float bulletSpeed = 10f;
	[SerializeField]
	float lifeTimeBullet = 3;

	void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	void OnEnable ()
	{
        rigid.WakeUp();
        rigid.isKinematic = false;
		rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Invoke("lifeTime", lifeTimeBullet);
    }

    void OnDisable()
	{
		rigid.Sleep();
		CancelInvoke();
	}

	void lifeTime()
	{
        gameObject.SetActive(false);	
	}

	void OnCollisionEnter(Collision other)
	{
		gameObject.SetActive(false);
	}
}

