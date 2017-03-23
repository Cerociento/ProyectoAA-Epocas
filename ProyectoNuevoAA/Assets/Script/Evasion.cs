using UnityEngine;
using System.Collections;

public class Evasion : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    [SerializeField]
    float distance;
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Evade();
        }
    }

    void Evade()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, distance))
        {
             transform.position = hit.point;
        }

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue,10);
    }

}
