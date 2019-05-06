﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;
    private void Awake()
    {
        instance = this;
    }
    public Sprite miniGarbage;
    public Sprite truckSprite;
    public Sprite[] garbageSpriteArray = new Sprite[Constants.totalGarbageElements];
    public string[] garbageName = new string[Constants.totalGarbageElements];
    public Sprite diposalText;


}
