using UnityEngine;
using System.Collections;

public class CameraMov : MonoBehaviour
{ 
     [SerializeField]
     float speed;
     float speedStart;
     [SerializeField]
     Transform target;
     [SerializeField]
     Vector3 camPosition;
     public bool active;
     Camera cam;
     Plane[] planes;

     void Start()
     {
        speedStart = speed;
     }

    void LateUpdate()
     {
         if (active)
         {
             // speed = speedStart;
             if (speed <= speedStart)
                 speed += Time.deltaTime;
             camPosition.y = 15;
             transform.position = Vector3.Lerp(transform.position, target.position + camPosition, Time.deltaTime * speed);
         }
         else
         {
             camPosition.y = 20;
             transform.position = Vector3.Lerp(transform.position, target.position + camPosition, Time.deltaTime * speed);
             speed -= Time.deltaTime * 3;
             if (speed <= 0)
                 speed = 0;
         }

         if(!GameObject.FindObjectOfType(typeof(EnemyBehaviour)))
         {
             active = true;
         }

         if(!target.GetChild(0).GetComponent<Renderer>())
         {
             Debug.Log("bye player");
         }

     }

    void OnTriggerExit(Collider other)
     {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Movement>().rig.velocity = Vector3.zero;
            Debug.Log("Bye");
        }
    }
}
