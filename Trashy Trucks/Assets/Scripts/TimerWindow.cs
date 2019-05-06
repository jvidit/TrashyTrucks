using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerWindow : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    public Truck truck;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (truck.currentTimerPower > 0)
            startTime += Time.deltaTime;
        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.SetText(minutes + ":" + seconds);



        if (minutes.Equals("5"))
            Loader.Load(Loader.Scene.EndScene);
    }
}
