using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPower : MonoBehaviour
{

    private float maxTimerPower;
    public Truck truck;
    public Slider powerTimerBar;


    // Start is called before the first frame update
    void Start()
    {

        maxTimerPower = 100f;
        truck.currentTimerPower = 0; ;
        powerTimerBar.value = CalculateTimerPower();
    }

    // Update is called once per frame
    void Update()
    {

        truck.currentTimerPower =Mathf.Max( truck.currentTimerPower - Time.deltaTime * 75*(Constants.consumptionRate),0);
        powerTimerBar.value = CalculateTimerPower();

    }

    float CalculateTimerPower()
    {
        return truck.currentTimerPower / maxTimerPower;
    }
}
