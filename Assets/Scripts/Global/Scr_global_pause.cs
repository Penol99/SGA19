using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_global_pause : MonoBehaviour
{
    
    public static bool m_isPaused;
    public static bool m_canPause = true;

    private static bool m_openPauseMenu;
    private static Canvas m_pauseCanvas;

    private void Awake()
    {
        InitiatePauseMenu();   
    }

    private void InitiatePauseMenu()
    {
        m_pauseCanvas = FindObjectOfType<Scr_pause_menu>().GetComponent<Canvas>();
        m_pauseCanvas.gameObject.SetActive(false);
        m_pauseCanvas.enabled = true;
    }


    private void Update()
    {
        if (m_canPause && Input.GetButtonDown("Start"))
        {
            m_openPauseMenu = !m_isPaused;
            SetPauseGame(!m_isPaused);
            
            
        }
    }


    public static void SetPauseGame(bool enabled)
    {
        m_isPaused = enabled;
        Time.timeScale = Scr_convert.ToInt(!m_isPaused);
        Scr_player_controller.FreezePlayer = m_isPaused;
        foreach (Animator anim in Scr_global_lists.AnimatorList)
        {
            anim.speed = Scr_convert.ToInt(!m_isPaused);
        }

        m_pauseCanvas.enabled = m_openPauseMenu;

    }


}
