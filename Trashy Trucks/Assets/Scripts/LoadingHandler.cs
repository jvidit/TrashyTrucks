using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHandler : MonoBehaviour
{
    private int i;


    void Start()
    {
        i = 0;
    }

    private void Update()
    {
        if (i == 80)
            Loader.Load(Loader.Scene.MainGame);
        i++;
    }



}
