using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader 
{


    public enum Scene { MainGame, LoadingScene, EndScene };

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }


}
