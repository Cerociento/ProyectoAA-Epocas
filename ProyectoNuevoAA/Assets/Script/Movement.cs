using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    [SerializeField]
    float movementSpeed = 18;
    [SerializeField]
    float zAxis;
    [SerializeField]
    float xAxis;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float slashDistance = 1;

    RaycastHit hit;
    Vector3 pointDirection;
    public Rigidbody rig;
    Vector3 newPosition;
    Vector3 mov;
    Animator anim;
    bool inGround;

    public static Vector3 pointEvasion;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	void FixedUpdate ()
    {
        Move();
        pointEvasion = pointDirection;
        Slash();
    }

    void OnCollisionEnter(Collision hit)
    {
       rig.velocity = mov;
       inGround = true;
    }

    void Move()
    {
        zAxis = Input.GetAxisRaw("Vertical");
        xAxis = Input.GetAxisRaw("Horizontal");
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePosition, out hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                pointDirection = hit.point;
                transform.LookAt(new Vector3(pointDirection.x, transform.position.y, pointDirection.z));
            }
        }

        mov = new Vector3(xAxis, -0.1f, zAxis) * movementSpeed;
        rig.velocity = mov;
        rig.MovePosition(transform.position);

        if (Input.GetButtonDown("Jump"))
        {
            Dodge();
            inGround = true;
        }
    }

    void Dodge()
    {
        if (transform.position.y <= 0)
        {
            rig.velocity = new Vector3(xAxis, jumpForce, zAxis) * movementSpeed;
            rig.AddForce(Vector3.up, ForceMode.Impulse);    
            anim.SetTrigger("Dodge");
            inGround = false;
        }
    }

    void Slash()
    {
        Ray ray;
        RaycastHit hitSlash;
        ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);
        if(Physics.Raycast(ray,out hitSlash,slashDistance))
        {
            if(hitSlash.collider.CompareTag("Enemy"))
            {
                anim.SetTrigger("Slash");
                hitSlash.transform.gameObject.SetActive(false);
                Debug.Log("Cuchillada");
            }
        }
      //  Debug.DrawRay(ray.origin, ray.direction * slashDistance, Color.blue);
    }
}
