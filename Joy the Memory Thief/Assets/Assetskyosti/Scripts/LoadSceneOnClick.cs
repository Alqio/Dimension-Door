using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadScene()
    {
        int level = GameState.instance.level;
        int sceneId = 0;
        
        if (level == 0)
        {
            sceneId = 1;
        } else if (level == 1)
        {
            sceneId = 2;
        } else if (level == 2)
        {
            sceneId = 3;
        }
        if (GameState.instance.isInHub)
        {
            sceneId = 1;
        }
        Debug.Log(level);
        Debug.Log(sceneId);

        SceneManager.LoadScene(sceneId);
    }
}
