using UnityEngine;
using System.Collections;

public class CameraMov : MonoBehaviour
{ 
     [SerializeField]
     float speed;
     float speedStart;
     [SerializeField]
     float zoomSpeed;
     float zoomSpeedStart;
     [SerializeField]
     Transform target;
     [Tooltip("Ajusta posicion de la cámara")]
     [SerializeField]
     Vector3 camPosition;
     [Tooltip("Posicion de la camara al bloquearse.")]
     public static Vector3 camLockedPosition;
     public bool active;
     Camera cam;
     Plane[] planes;

     void Start()
     {
        speedStart = speed;
        zoomSpeedStart = zoomSpeed;
     }

    void LateUpdate()
     {
         if (active)
         {
            if (speed <= speedStart)
            {
                speed += Time.deltaTime;
                zoomSpeed = zoomSpeedStart;
            }

            if (target.position.z > transform.position.z + 2)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + camPosition, Time.deltaTime * speed);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, 0) + new Vector3(camPosition.x, camPosition.y, transform.position.z), Time.deltaTime * speed);
            }            
         }
         else if(!active)
         {
             transform.position = Vector3.Lerp(transform.position, camLockedPosition, Time.deltaTime * zoomSpeed);
             speed -= Time.deltaTime;
             if (speed <= 0)
            {
                speed = 0;
            }
         }

         if(!GameObject.FindObjectOfType(typeof(EnemyBehaviour)))
         {
             active = true;
         }
    }
}
