using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSceneHandler : MonoBehaviour
{
    private int i;
    public TextMeshProUGUI pts;

    void Start()
    {
        i = 0;
        pts.SetText((Truck.points).ToString());
    }

    private void Update()
    {
        if (i == 80)
            Loader.Load(Loader.Scene.LoadingScene);
        i++;
    }



}
