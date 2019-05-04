using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private Vector2 gridMoveDirection;
    private Vector2 gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;


    private void Awake()
    {
        gridPosition = new Vector2(0, 0);
        gridMoveTimerMax = 0.01f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2(0, 1);

    }



    // Update is called once per frame
    private void Update()
    {

        HandleInput();
        HandleGridMovement();
    }





    private float GetAngleFromVector(Vector2 dir)
    {
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (ang < 0)
            ang += 360;
        return ang - 90;
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridPosition += gridMoveDirection*gridMoveTimerMax;
            gridMoveTimer -= gridMoveTimerMax;
        }
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gridMoveDirection.x = 0;
            gridMoveDirection.y = 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridMoveDirection.x = 0;
            gridMoveDirection.y = -1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridMoveDirection.x = -1;
            gridMoveDirection.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridMoveDirection.x = 1;
            gridMoveDirection.y = 0;
        }


    }

}





