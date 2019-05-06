using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{

    private float maxFuel;
    public Truck truck;
    public Slider fuelBar;
    

    // Start is called before the first frame update
    void Start()
    {
        maxFuel = 100f;
        truck.currentFuel = maxFuel;
        fuelBar.value = CalculateFuel(); 
    }

    // Update is called once per frame
    void Update()
    {

        truck.currentFuel = Mathf.Max(truck.currentFuel - (truck.correctGarbages + truck.incorrectGarbages + Constants.innateConsumption) * Time.deltaTime * (Constants.consumptionRate),0);
        fuelBar.value = CalculateFuel();

    }

    float CalculateFuel()
    {
        return truck.currentFuel / maxFuel;
    }
}
