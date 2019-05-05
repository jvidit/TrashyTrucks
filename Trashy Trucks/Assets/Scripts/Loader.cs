using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader 
{


    public enum Scene { MainGame, LoadingScene };

    public static void Load(Scene scene)
    {
        Debug.Log("Being called");



        Debug.Log("Being called2");
        SceneManager.LoadScene(scene.ToString());
    }


}
