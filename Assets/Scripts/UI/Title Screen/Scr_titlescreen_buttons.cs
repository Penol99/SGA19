using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_titlescreen_buttons : MonoBehaviour
{

    private void Start()
    {
        
    }


    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        PlayerPrefs.SetInt("Continue", 1);
        PlayerPrefs.Save();
        int sceneIndex = PlayerPrefs.GetInt("SceneIndex");
        SceneManager.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {

    }
}
