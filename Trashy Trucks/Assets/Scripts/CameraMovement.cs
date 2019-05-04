using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   public Transform truck;

    

    void Update()
    {

        transform.position = new Vector3(truck.position.x, truck.position.y,-20);
    }
}
