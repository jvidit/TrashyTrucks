using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Truck : MonoBehaviour
{
    private Vector2 gridMoveDirection;
    private Vector2 gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelGrid;
    public int correctGarbages;
    public int incorrectGarbages;
    public GameObject PopUpUI;
    public float currentFuel;
    public float currentTimerPower;
    public float currentClassifyPower;
    public static int points;
    public TextMeshProUGUI pointsText;
    private int isBiodegradable;
    public bool isPopUpSet = false;
    private float truckAng = 0;
    public Image garbageImage;
    public TextMeshProUGUI garbageName;

    private void Awake()
    {
        gridPosition = new Vector2(0, 0);
        gridMoveTimerMax = 0.01f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2(0, 1);
        correctGarbages = 0;
        incorrectGarbages = 0;
        points = 0;
        truckAng = 0;
        
    }

    // Update is called once per frame
    private void Update()
    {

        HandleInput();
        HandleGridMovement();
        LookForTrash();
        pointsText.SetText(points.ToString());

        if (currentFuel<=0)
            Loader.Load(Loader.Scene.EndScene);

        transform.eulerAngles = new Vector3(0, 0, truckAng - 90);

       
        

    }


    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
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

            gridPosition += (new Vector2(Mathf.Cos(truckAng*Mathf.Deg2Rad),Mathf.Sin(truckAng*Mathf.Deg2Rad)))*gridMoveTimerMax*Constants.truckSpeed;
            gridMoveTimer -= gridMoveTimerMax;
            gridPosition += new Vector2(0, 0);
        }
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        //transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

        gridPosition = ValidateGridPosition(gridPosition);


    }

    private void LookForTrash()
    {
        string action = (levelGrid.detectGarbage(gridPosition));
        string action2 = (levelGrid.detectPowerUp(gridPosition));

        if (action.Equals("increaseCorrectGarbage"))
            correctGarbages += 1;
        else if (action.Equals("increaseIncorrectGarbage"))
            incorrectGarbages += 1;
        else if (action.Equals("empty"))
        {
            currentFuel += correctGarbages * Constants.correctFuelUp + incorrectGarbages * Constants.incorrectFuelUp;
            points += correctGarbages * Constants.correctPointsUp + incorrectGarbages * Constants.incorrectPointsUp ;


            correctGarbages = 0;
            incorrectGarbages = 0;
        }

        if (action2.Equals("0"))
            currentClassifyPower = 100;
        if (action2.Equals("1"))
            currentFuel = 100;
        if (action2.Equals("2"))
            currentTimerPower = 100;

        if(!(action2.Equals("noChange")))Debug.Log(action2);
    }

    private void HandleInput()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            truckAng += Constants.angularVelocity;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            truckAng -= Constants.angularVelocity;
        }


    }

    public Vector2 ValidateGridPosition(Vector2 gridPosition)
    {
        if (gridPosition.x < -(levelGrid.width-1))
        {
            gridPosition.x = levelGrid.width - 1;
        }

        if (gridPosition.y < -(levelGrid.height-1))
        {
            gridPosition.y = levelGrid.height - 1;
        }

        if (gridPosition.x >= levelGrid.width)
        {
            gridPosition.x = -(levelGrid.width - 1);
        }

        if (gridPosition.y >= levelGrid.height)
        {
            gridPosition.y = -(levelGrid.height - 1);
        }

        return gridPosition;
    }

    public string Classify(int isBiodegradable, Sprite sprite,  string garbageNameString)
    {
        this.isBiodegradable = isBiodegradable;
        garbageName.SetText(garbageNameString);
        PopUp(sprite);
        return "noChange";

    }


    private void UnPopUp()
    {
        isPopUpSet = false;
        PopUpUI.SetActive(false);
        Time.timeScale = 1f;

    }



    private void PopUp(Sprite sprite)
    {
        isPopUpSet = true;
        float width = 100* sprite.bounds.size.x;  // this is gonna be our width
        float height = 100 * sprite.bounds.size.y;  // this is gonna be our height
        garbageImage.rectTransform.sizeDelta = new Vector2(width, height);
        garbageImage.sprite= sprite;
        PopUpUI.SetActive(true);
        Time.timeScale = 0.5f;
    }

    public void green()
    {
        if (isBiodegradable == 1)
            correctGarbages++;
        else
            incorrectGarbages++;
        UnPopUp();
    }

    public void blue()
    {
        if (isBiodegradable == 0)
            correctGarbages++;
        else
            incorrectGarbages++;
        UnPopUp();
    }



}





