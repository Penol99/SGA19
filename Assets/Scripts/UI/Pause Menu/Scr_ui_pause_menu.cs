using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_ui_pause_menu : MonoBehaviour
{
    public Scr_ui_panel m_exitPanel;
    public GameObject m_mainButtons;

    private Scr_ui_panel m_panel;

    private void OnEnable()
    {
        m_panel = GetComponent<Scr_ui_panel>();
    }

    public void OnPauseOpen()
    {
        m_exitPanel.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (m_exitPanel.gameObject.activeInHierarchy)
            {
                m_exitPanel.gameObject.SetActive(false);
                m_panel.SetSelectedButton(m_panel);
                m_panel.EnableButtonInteraction(m_mainButtons);
            }
        }
    }


    public void ItemsButton()
    {

    }

    public void EquipmentButton()
    {

    }

    public void SettingsButton()
    {

    }

    #region EXIT PANEL BUTTONS
    public void ExitToTitleButton()
    {
        Scr_global_pause.SetPauseGame(false);
        SceneManager.LoadScene(0);
        
    }

    public void QuitGameButton()
    {
        Scr_global_pause.SetPauseGame(false);
        Application.Quit();
    }

    #endregion

    



}
