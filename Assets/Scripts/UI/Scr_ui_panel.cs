using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scr_ui_panel : MonoBehaviour
{
    public GameObject m_firstSelectedObject;
    [Header("If close on cancel button")]
    public bool m_closeOnCancelButton;
    public Scr_ui_panel m_toEnableOnCancel;



    private void Update()
    {
        if (m_closeOnCancelButton)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                
                gameObject.SetActive(false);
                if (m_toEnableOnCancel != null)
                {
                    m_toEnableOnCancel.gameObject.SetActive(true);
                    SetSelectedButton(m_toEnableOnCancel);
                }
            }
        }
    }

    public void OpenPanel(Scr_ui_panel panel)
    {    
        panel.gameObject.SetActive(true);
        SetSelectedButton(panel);

    }

    public void PlayerFreezeState(bool value)
    {
        Scr_player_controller.FreezePlayer = value;
    }

    public void SetSelectedButton(Scr_ui_panel panel)
    {
        GameObject firstSelected = panel.m_firstSelectedObject;
        Scr_global_canvas.MainEventSystem.SetSelectedGameObject(firstSelected);
    }

    private void SetButtonsInteractable(GameObject buttonParent, bool value)
    {
        Button[] buttons = buttonParent.GetComponentsInChildren<Button>();

        foreach (var b in buttons)
        {
            b.interactable = value;
        }
    }

    public void DisableButtonInteraction(GameObject buttonParent)
    {
        SetButtonsInteractable(buttonParent, false);
    }
    public void EnableButtonInteraction(GameObject buttonParent)
    {
        SetButtonsInteractable(buttonParent, true);
    }



}
