using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScreen : MonoBehaviour
{
    float leftConstraint = 0.0f;
    float rightConstraint = 960.0f;
    float buffer = 1.0f;

    private GameObject[] blocks;

    float distanceZ;
    //public Camera cam;

    void Awake()
    {
        //cam = Camera.main;
        /*
        distanceZ = Mathf.Abs(cam.transform.position.z - transform.position.z);

        

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width * 0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width * 1.0f, 0.0f, distanceZ)).x;
        */
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

    public void setup(float distance, float left, float right)
    {
        distanceZ = distance;
        leftConstraint = left;
        rightConstraint = right;
    }
}