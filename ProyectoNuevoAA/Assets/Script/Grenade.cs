using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {
    bool inGround;
    Color col;
    [SerializeField]
    float countExplosion = 2;
    [SerializeField]
    float launchForce = 2;
    [SerializeField]
    float radiusExplosion = 0.3f;
    float privateCountExplosion;
    [SerializeField]
    float alfaDuration = 0.5f;
    [SerializeField]
    GameObject particleExplosion;

    Rigidbody rig;
    Transform barrel;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        barrel = GameObject.Find("GrenadeBarrel").transform;
    }

    void OnEnable()
    {
        privateCountExplosion = countExplosion;
        rig.isKinematic = false;
        GetComponent<Renderer>().enabled = true;
        rig.AddForce(barrel.forward * launchForce , ForceMode.Impulse);
        transform.GetChild(0).GetComponent<SphereCollider>().radius = radiusExplosion;
    }

    void Update ()
    {
        if (inGround)
        {
            float lerp = Mathf.PingPong(Time.time, alfaDuration) / alfaDuration;
            col = Color.Lerp(Color.red, Color.yellow, lerp);
            transform.GetComponent<Renderer>().material.color = col;
            privateCountExplosion-=Time.deltaTime;
            if (privateCountExplosion <= 0)
            {
                GetComponent<Renderer>().enabled = false;
                transform.GetChild(0).GetComponent<SphereCollider>().radius += radiusExplosion;
                particleExplosion.SetActive(true);
                rig.Sleep();
                Invoke("Desactive", 1);
            }
        }
	}

    void OnDisable()
    {
        rig.Sleep();
        CancelInvoke();
    }

    void Desactive()
    {
        inGround = false;
        particleExplosion.SetActive(false);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision hit)
    {
        inGround = true;
    }
}
