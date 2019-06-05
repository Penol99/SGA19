using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Scr_global_pause : MonoBehaviour
{
    
    public static bool IsPaused;
    public static bool CanPause = true;

    private static bool OpenPauseMenu;
    private static GameObject PausePanel;

    private Scr_save_load_game m_saveManager;



    private void Start()
    {
        m_saveManager = FindObjectOfType<Scr_save_load_game>();
        PausePanel = Scr_global_canvas.PauseMenuPanel;
    }

    private void Update()
    {
        if (CanPause && Input.GetButtonDown("Start"))
        {
            OpenPauseMenu = !IsPaused;
            SetPauseGame(!IsPaused);
            m_saveManager.SaveGame();
            if (!IsPaused)
                PausePanel.GetComponent<Scr_ui_pause_menu>().OnPauseOpen();

        }
    }


    public static void SetPauseGame(bool enabled)
    {
        IsPaused = enabled;
        Time.timeScale = Scr_convert.ToInt(!IsPaused);
        Scr_player_controller.FreezePlayer = IsPaused;
        foreach (Animator anim in Scr_global_lists.AnimatorList)
        {
            anim.speed = Scr_convert.ToInt(!IsPaused);
        }
        // Open pause menu
        Scr_global_canvas.SetPanelActive(PausePanel,OpenPauseMenu,true);

    }




}
