using UnityEngine;
using System.Collections;

public class BulletForce : MonoBehaviour {

    Rigidbody rig;
    [SerializeField]
    float bulletSpeed = 10;
    [SerializeField]
    float lifeTimeBullet = 3;
    [SerializeField]
    Transform barrel;
    [SerializeField]
    bool bounce;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        barrel = GameObject.Find("Barrel").transform;
    }

	void OnEnable ()
    {
        rig.WakeUp();
        rig.isKinematic = false;
        rig.AddForce(barrel.forward * bulletSpeed, ForceMode.Impulse);
        Invoke("lifeTime",lifeTimeBullet);
    }

    void OnDisable()
    {
        rig.Sleep();
        CancelInvoke();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!bounce)
        {
            if (!other.collider.transform.GetComponent<BulletForce>())
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (other.collider.CompareTag("Enemy")|| other.collider.CompareTag("ForceField"))
            {
                gameObject.SetActive(false);
            }
            rig.AddForce(transform.forward, ForceMode.Force);
        }
    }

    void lifeTime()
    {
        gameObject.SetActive(false);
    }
}
