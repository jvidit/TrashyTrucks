using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private Vector2Int garbageGridPosition;
    private int width;
    private int height;
    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        SpawnGarbage();
    }
    public void SpawnGarbage()
    {
        garbageGridPosition = new Vector2Int(Random.Range(-width, width), Random.Range(-height,height));
        GameObject garbageGameObject = new GameObject("Garbage", typeof(SpriteRenderer));
        garbageGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.garbageSpriteArray[Random.Range(0,Constants.totalGarbageElements)];
        garbageGameObject.transform.position = new Vector3(garbageGridPosition.x, garbageGridPosition.y);
        
    }





 
}
