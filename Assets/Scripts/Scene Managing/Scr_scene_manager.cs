using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_scene_manager : MonoBehaviour
{
    private static Vector3 SpawnPos;
    private static bool SpawnOnPos = false;
    private Scr_player_movement m_player;
    private Animator m_anim;
    private string m_sceneToLoad;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_player = FindObjectOfType<Scr_player_movement>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SpawnOnPos)
        {
            m_player.GotoPosition(SpawnPos);
            SpawnOnPos = false;
            SpawnPos = Vector3.zero;
        }

    }



    public void LoadScene(string scene,Vector3 spawnPos)
    {
        m_sceneToLoad = scene;
        m_anim.SetTrigger("fadeIn");
        Scr_player_controller.FreezePlayer = true;
        SpawnPos = spawnPos;

        SpawnOnPos = true;
    }

    public void FadeInCompleted()
    {
        if (!m_sceneToLoad.Equals(""))
            SceneManager.LoadScene(m_sceneToLoad);
        m_sceneToLoad = "";
        Scr_player_controller.FreezePlayer = false;
    }

}
