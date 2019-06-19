using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_ui_animator_events : MonoBehaviour
{
    private Scr_scene_manager m_sceneManager;
    private Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_sceneManager = FindObjectOfType<Scr_scene_manager>();
        m_player = FindObjectOfType<Scr_player_controller>().transform;
    }


    public void ReloadSceneAtPlayerPosition()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        m_sceneManager.LoadScene(currentScene, m_player.position);
    }
}
