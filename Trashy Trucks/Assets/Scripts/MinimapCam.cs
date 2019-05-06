using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    public Transform truck;



    void Update()
    {
        transform.eulerAngles = truck.transform.eulerAngles;
    }
}
