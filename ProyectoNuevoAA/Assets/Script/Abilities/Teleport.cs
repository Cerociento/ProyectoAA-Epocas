using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    Vector3 pointDirection;
    Color col;

    void Start()
    {
        col = transform.GetChild(0).GetChild(0).GetComponent<TrailRenderer>().material.color;
    }

    void Update ()
    {
        pointDirection = Movement.pointEvasion;
   
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Evade();
        }
        col.a -= 0.10f;
        transform.GetChild(0).GetChild(0).GetComponent<TrailRenderer>().material.color = col;
    }

    void Evade()
    {
        if (col.a <= 0)
        {
            transform.position = pointDirection;
            col.a = 1f;
        }
    }
}
