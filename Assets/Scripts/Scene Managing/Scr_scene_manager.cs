using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_scene_manager : MonoBehaviour
{

    private static Vector3 SpawnPos;
    private static bool SpawnOnPos = false;
    private Scr_player_controller m_playerCon;
    private Animator m_anim;
    private string m_sceneToLoad;
    
    

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_playerCon = FindObjectOfType<Scr_player_controller>();
    }
    private void Start()
    {
        if (SpawnOnPos)
        {
            m_playerCon.transform.position = SpawnPos;
            SpawnOnPos = false;
            SpawnPos = Vector3.zero;
        }
    }


    public void LoadScene(string scene,Vector3 spawnPos)
    {
        m_sceneToLoad = scene;
        m_anim.SetTrigger("fadeIn");
        m_playerCon.PlayerFreeze(true);
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
