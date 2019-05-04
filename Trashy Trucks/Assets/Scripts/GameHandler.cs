using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    private LevelGrid levelGrid;
    private float garbageTimer;
    private float garbageTimerMax;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.start");
        levelGrid = new LevelGrid(20, 20);
        garbageTimerMax = 5f;
        garbageTimer = garbageTimerMax;
    }

    private void Update()
    {
        garbageTimer += Time.deltaTime;

        if (garbageTimer >= garbageTimerMax)
        {
            garbageTimer -= garbageTimerMax;
            levelGrid.SpawnGarbage();
        }
        
    }


}
