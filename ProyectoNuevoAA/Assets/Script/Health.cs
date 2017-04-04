using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	[SerializeField]
	float health;
	[SerializeField]
	float maxHealth;
    [SerializeField]
    bool isPlayer;
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
		if(col.collider.CompareTag("BulletEnemy") && isPlayer && !noEnemyDamage)
        {
			col.collider.gameObject.SetActive(false);
			health--;
            noEnemyDamage = true;
		}
        if(col.collider.CompareTag("Water") && isPlayer)
        {
            health = health - health;
            noEnemyDamage = true;
        }
		if(col.collider.CompareTag("BulletEnemy") && noEnemyDamage)
		{
			col.collider.gameObject.SetActive(false);
		}
        else if(!isPlayer && col.collider.CompareTag("Bullet"))
        {
            health--;
        }

        if (!isPlayer && col.collider.CompareTag("Grenade"))
        {
            health = health - 2;
        }

         if (health<=0)
        {
            if(!isPlayer)
            {
                transform.GetComponent<WeaponDrop>().Drop();
            }
			gameObject.SetActive(false);
		}
	}

    void PlayerDamage()
    {
        if (isPlayer)
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
		if(other.gameObject.CompareTag("Medipack")&& health<maxHealth && isPlayer){
			health++;
			other.gameObject.SetActive(false);
		}
	}
}
