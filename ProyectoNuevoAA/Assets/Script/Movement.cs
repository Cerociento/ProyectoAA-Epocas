using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    [SerializeField]
    float movementSpeed = 18;
    [SerializeField]
    float zAxis;
    [SerializeField]
    float xAxis;
    RaycastHit hit;
    Vector3 pointDirection;
    Rigidbody rig;
    bool dodge = false;
    Vector3 newPosition;
    Vector3 mov;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        Color col = transform.GetChild(0).GetComponent<TrailRenderer>().material.color;
        zAxis = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePosition, out hit))
        {
            if(!hit.collider.CompareTag("Player"))
            {
                pointDirection = hit.point;
                transform.LookAt(new Vector3(pointDirection.x, transform.position.y, pointDirection.z));
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (col.a <= 0)
                {
                    transform.position = pointDirection;
                    col.a = 1f;
                    transform.GetChild(0).GetComponent<TrailRenderer>().material.color = col;
                }
            }
        }
        col.a -= 0.10f;
        transform.GetChild(0).GetComponent<TrailRenderer>().material.color = col;
        mov = new Vector3(xAxis, 0, zAxis) * movementSpeed;
        rig.velocity = mov;
        rig.MovePosition(transform.position);
    }

    void OnCollisionEnter(Collision hit)
    {
        rig.velocity = mov;
    }


}
