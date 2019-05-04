using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private Vector2Int garbageGridPosition;
    private GameObject garbageGameObject;
    private List<GameObject> garbageObjectArray = new List<GameObject>();
    public int width;
    public int height;
    private Truck truck;



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
        garbageGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.garbageSpriteArray[Random.Range(0,Constants.totalGarbageElements)];
        garbageGameObject.transform.position = new Vector3(garbageGridPosition.x, garbageGridPosition.y);



        garbageObjectArray.Add(garbageGameObject);
    }


    public void TruckMoved(Vector2 truckGridPosition)
    {
        foreach (GameObject garbageElement in garbageObjectArray)
        {
            Vector2Int garbageElementGridPosition = v3tov2int(garbageElement.transform.position);
            if ((truckGridPosition - garbageElementGridPosition).magnitude < Constants.pickupDistance)
            {
                garbageObjectArray.Remove(garbageElement);
                Object.Destroy(garbageElement);
                break;
            }
        }
    }

    public void Setup(Truck truck)
    {
        this.truck = truck;
    }




  



    private Vector2Int v3tov2int (Vector3 v)
    {
        Vector2Int vector2 = new Vector2Int();
        vector2.x = (int)v.x;
        vector2.y = (int)v.y;
        return vector2;
    }

}
