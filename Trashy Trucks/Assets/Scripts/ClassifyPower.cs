using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassifyPower : MonoBehaviour
{

    private float maxClassifyPower;
    public Truck truck;
    public Slider powerClassifyBar;


    // Start is called before the first frame update
    void Start()
    {

        maxClassifyPower = 100f;
        truck.currentClassifyPower = 0; ;
        powerClassifyBar.value = CalculateClassifyPower();
    }

    // Update is called once per frame
    void Update()
    {

        truck.currentClassifyPower = Mathf.Max(truck.currentClassifyPower - Time.deltaTime * 75 * (Constants.consumptionRate), 0);
        powerClassifyBar.value = CalculateClassifyPower();

    }

    float CalculateClassifyPower()
    {
        return truck.currentClassifyPower / maxClassifyPower;
    }
}
