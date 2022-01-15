using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void LateUpdate()
    {
        //current position of the camera (temp)
        Vector3 temp = playerTransform.position;

        //set cameras position x to be equal to players position
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;


        //set back the camera's temp position to the cam's current position
        playerTransform.position = temp;
    }
}
