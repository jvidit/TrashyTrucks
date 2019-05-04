using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;
    private void Awake()
    {
        instance = this;
    }
    public Sprite truckSprite;
    public Sprite[] garbageSpriteArray = new Sprite[Constants.totalGarbageElements];


}
