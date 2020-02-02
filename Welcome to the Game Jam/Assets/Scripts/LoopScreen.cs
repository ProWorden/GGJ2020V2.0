using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScreen : MonoBehaviour
{
    public float leftConstraint = 0.0f;
    public float rightConstraint = 960.0f;
    public float buffer = 1.0f;

    private GameObject[] blocks;

    public float distanceZ;
    public Camera cam;

    void Awake()
    {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z - transform.position.z);

        

        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.0f, 0.0f, distanceZ)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1.0f, 0.0f, distanceZ)).x;
    }

    void FixedUpdate()
    {

        blocks = GameObject.FindGameObjectsWithTag("Block");
        for (int i =0; i< blocks.Length; i++)
        {
            if (blocks[i].transform.position.x < leftConstraint - buffer)
            {
                blocks[i].transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
            }
            if (blocks[i].transform.position.x > rightConstraint + buffer)
            {
                blocks[i].transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
            }
        }

       
    }
}