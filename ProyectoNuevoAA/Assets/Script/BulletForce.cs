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
        CancelInvoke();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!bounce)
        {
            if (!other.collider.transform.GetComponent<BulletForce>())
            {
                rig.Sleep();
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (other.collider.CompareTag("Player")|| other.collider.CompareTag("ForceField"))
            {
                rig.Sleep();
                gameObject.SetActive(false);
            }
            rig.AddForce(transform.forward, ForceMode.Force);
        }
    }

    void lifeTime()
    {
        rig.Sleep();
        gameObject.SetActive(false);
    }
}
