using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_titlescreen_buttons : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        PlayerPrefs.SetInt("Continue", 1);
        Debug.Log(PlayerPrefs.GetInt("SceneIndex"));
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
