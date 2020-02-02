using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrolling : MonoBehaviour
{
    public Transform target;

     void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(transform.position.x, target.position.y +5 , transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }

}
