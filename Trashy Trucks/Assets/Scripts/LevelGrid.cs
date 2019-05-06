using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGrid 
{
    private Vector2Int garbageGridPosition;
    private GameObject garbageGameObject;
    private GameObject miniGarbageGameObject;
    private List<GarbageElement> garbageObjectArray = new List<GarbageElement>();
    private List<GameObject> disposalPopArray = new List<GameObject>();

    private Vector2Int powerGridPosition;
    private GameObject powerGameObject;
    private GameObject miniPowerGameObject;
    private List<PowerUpElement> powerUpArray = new List<PowerUpElement>();


    private GameObject disposalPopUp;


    private GameObject dustbin1;
    private GameObject dustbin2;



    public int width;
    public int height;
    private Truck truck;




   


    public void Setup(GameObject dustbin1,GameObject dustbin2,Truck truck)
    {
        this.truck = truck;
        this.dustbin1 = dustbin1;
        this.dustbin2 = dustbin2;

    }


   

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        SpawnGarbage();


    }

    public void SpawnGarbage()
    {
        garbageGridPosition = new Vector2Int(Random.Range(-width+Constants.boundary, width-Constants.boundary), Random.Range(-height+Constants.boundary,height-Constants.boundary));
        garbageGameObject = new GameObject("Garbage", typeof(SpriteRenderer));
        miniGarbageGameObject = new GameObject("MiniGarbage", typeof(SpriteRenderer));

        int index = Random.Range(0, Constants.totalGarbageElements);
        garbageGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.garbageSpriteArray[index];
        garbageGameObject.transform.position = new Vector3(garbageGridPosition.x, garbageGridPosition.y);
        
        miniGarbageGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.miniGarbage;
        miniGarbageGameObject.transform.position = new Vector3(garbageGridPosition.x, garbageGridPosition.y);



        GarbageElement ge = new GarbageElement();
        ge.garbageElement = garbageGameObject;
        ge.miniGarbage = miniGarbageGameObject;
        ge.miniGarbage.layer = LayerMask.NameToLayer("minimap");
        if (index < Constants.totalGarbageElements / 2)
            ge.isBiodegradable = 1;
        else
            ge.isBiodegradable = 0;

        ge.garbageName = GameAssets.instance.garbageName[index];
        garbageObjectArray.Add(ge);
    }


    public void SpawnPowerUp()
    {
        powerGridPosition = new Vector2Int(Random.Range(-width + Constants.boundary, width - Constants.boundary), Random.Range(-height + Constants.boundary, height - Constants.boundary));
        powerGameObject = new GameObject("PowerUp", typeof(SpriteRenderer));
        miniPowerGameObject = new GameObject("MiniPower", typeof(SpriteRenderer));

        int index = Random.Range(0, Constants.totalPowerUps);
        powerGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.powerUpsArray[index];
        powerGameObject.transform.position = new Vector3(powerGridPosition.x, powerGridPosition.y);

        miniPowerGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.miniPower;
        miniPowerGameObject.transform.position = new Vector3(powerGridPosition.x, powerGridPosition.y);


        PowerUpElement pe = new PowerUpElement();
        pe.powerElement = powerGameObject;
        pe.miniPower = miniPowerGameObject;
        pe.miniPower.layer = LayerMask.NameToLayer("minimap");
        pe.index = index;

        powerUpArray.Add(pe);


    }


    public void SpawnDisposal(Vector2 popUpPosition)
    {
        if (truck.incorrectGarbages + truck.correctGarbages == 0)
            return;

        disposalPopUp = new GameObject("DisposalPopUp", typeof(SpriteRenderer));
        disposalPopUp.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.diposalText;
        disposalPopUp.transform.position = new Vector3(popUpPosition.x, popUpPosition.y, 0);
        disposalPopUp.layer = LayerMask.NameToLayer("popup");
        disposalPopArray.Add(disposalPopUp);

    }



    public string TruckMoved(Vector2 truckGridPosition)
    {
        string action = "noChange";
        //Checking if garbage can be picked by the truck


        matchAngles();


        foreach (GarbageElement garbageIterator in garbageObjectArray)
        {
            Vector2Int garbageElementGridPosition = v3tov2int(garbageIterator.garbageElement.transform.position);
            Vector2Int dustbin1GridPosition = v3tov2int(dustbin1.transform.position);
            Vector2Int dustbin2GridPosition = v3tov2int(dustbin2.transform.position);

            if ((truckGridPosition - garbageElementGridPosition).magnitude < Constants.pickupDistance)
            {
                garbageObjectArray.Remove(garbageIterator);
                if (!truck.isPopUpSet)
                    action = truck.Classify(garbageIterator.isBiodegradable,garbageIterator.garbageElement.GetComponent<SpriteRenderer>().sprite,garbageIterator.garbageName); //Add Clasification
                else action = "noChange";


                Object.Destroy(garbageIterator.garbageElement);
                Object.Destroy(garbageIterator.miniGarbage);

                break;
            }
            if ((truckGridPosition - dustbin1GridPosition).magnitude < Constants.dropDistance)
            { 
                action = "empty";
                SpawnDisposal(dustbin1GridPosition);
            }
            if ((truckGridPosition - dustbin2GridPosition).magnitude < Constants.dropDistance)
            {
                action = "empty";
                SpawnDisposal(dustbin2GridPosition);
            }
        }

        return action;  



    }


    //matches angles of all objects with that of truck
    private void matchAngles()
    {

        foreach (GarbageElement garbageIterator in garbageObjectArray)
            garbageIterator.garbageElement.transform.eulerAngles = truck.transform.eulerAngles;

        foreach (PowerUpElement powerIterator in powerUpArray)
            powerIterator.powerElement.transform.eulerAngles = truck.transform.eulerAngles;


        dustbin1.transform.eulerAngles = truck.transform.eulerAngles;
        dustbin2.transform.eulerAngles = truck.transform.eulerAngles;

    }

    public void HandleDisposalPopUp()
    {
        foreach (GameObject disposalIterator in disposalPopArray)
        {
            disposalIterator.transform.eulerAngles = truck.transform.eulerAngles;
            disposalIterator.transform.position = new Vector3(disposalIterator.transform.position.x, disposalIterator.transform.position.y + Constants.popUpSpeed);
            if (disposalIterator.transform.position.y > 40)
            {
                disposalPopArray.Remove(disposalIterator);
                Object.Destroy(disposalIterator);
                break;
            }
        }
    }



    private Vector2Int v3tov2int (Vector3 v)
    {
        Vector2Int vector2 = new Vector2Int();
        vector2.x = (int)v.x;
        vector2.y = (int)v.y;
        return vector2;
    }



    

}
