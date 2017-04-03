using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	[SerializeField]
	float health;
	[SerializeField]
	float maxHealth;
    [SerializeField]
    bool noEnemy;
    public bool noEnemyDamage;
    float timeDamage = 2f;
    Color col;
    [SerializeField]
    float flickerSpeed = 0.5f;

    void Start()
    {
       col = transform.GetChild(0).GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        PlayerDamage();
    }

    void OnCollisionEnter(Collision col)
    {
		if(col.collider.CompareTag("BulletEnemy") && !noEnemyDamage)
        {
			col.collider.gameObject.SetActive(false);
			health--;
            noEnemyDamage = true;
		}
		if(col.collider.CompareTag("BulletEnemy") && noEnemyDamage)
		{
			col.collider.gameObject.SetActive(false);
		}
        else if(!noEnemy && col.collider.CompareTag("Bullet"))
        {
            health--;
        }

        if (!noEnemy && col.collider.CompareTag("Grenade"))
        {
            health = health - 2;
        }

         if (health<=0)
        {
			gameObject.SetActive(false);
		}
	}

    void PlayerDamage()
    {
        if (noEnemy)
        {
            if (noEnemyDamage)
            {
                float lerp = Mathf.PingPong(Time.time, flickerSpeed) / flickerSpeed;
                col.a = Mathf.Lerp(0, 1, lerp);
                transform.GetChild(0).GetComponent<Renderer>().material.color = col;
                timeDamage-= 1 * Time.deltaTime;
                if(timeDamage<=0)
                {
                    col.a = Mathf.Lerp(0, 1, 1);
                    transform.GetChild(0).GetComponent<Renderer>().material.color = col;
                    timeDamage = 2f;
                    noEnemyDamage = false;
                }
            }
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Medipack")&& health<maxHealth && noEnemy){
			health++;
			other.gameObject.SetActive(false);
		}
	}
}
