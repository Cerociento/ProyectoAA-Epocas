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
        Invoke("lifeTime", lifeTimeBullet);
		rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
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
			if (other.collider.CompareTag("Player"))
			{
				gameObject.SetActive(false);
			}
	}
	}

