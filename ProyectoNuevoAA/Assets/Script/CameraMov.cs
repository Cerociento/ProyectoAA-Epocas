using UnityEngine;
using System.Collections;

public class CameraMov : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 camPosition;
    [SerializeField]
    bool active;
    
    void LateUpdate()
    {
        if(active)
            transform.position = Vector3.Lerp(transform.position, target.position + camPosition, Time.deltaTime * speed);
    }
}
