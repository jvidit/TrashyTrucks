using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Truck truck;


    private LevelGrid levelGrid;
    public GameObject dustbin1;
    public GameObject dustbin2;

    private float garbageTimer;
    private float garbageTimerMax;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.start");

        levelGrid = new LevelGrid(20, 20);
        garbageTimerMax = 5f;
        garbageTimer = garbageTimerMax;

        truck.Setup(levelGrid);
        levelGrid.Setup(dustbin1,dustbin2,truck);

        truck.PopUpUI.SetActive(false);
    }

    private void Update()
    {
        garbageTimer += Time.deltaTime;

        if (garbageTimer >= garbageTimerMax)
        {
            garbageTimer -= garbageTimerMax;
            levelGrid.SpawnGarbage();
        }

        levelGrid.HandleDisposalPopUp();


    }


}
