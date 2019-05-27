using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_global_pause : MonoBehaviour
{
    public static bool m_isPaused;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
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

    }

}
