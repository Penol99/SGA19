using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scr_ui_panel : MonoBehaviour
{
    public GameObject m_firstSelectedObject;
    public bool m_freezePlayer;
    [Header("If close on cancel button")]
    public bool m_closeOnCancelButton;
    public bool m_unfreezePlayer;
    public bool m_disableParent;
    public Scr_ui_panel m_toEnableOnCancel;

    private Scr_player_controller m_pCon;

    private void OnEnable()
    {
        Scr_player_controller.FreezePlayer = m_freezePlayer;
        if (m_firstSelectedObject != null)
            SetSelectedButton(GetComponent<Scr_ui_panel>());
    }

    private void Start()
    {
        if (m_unfreezePlayer)
            m_pCon = FindObjectOfType<Scr_player_controller>();
    }

    private void Update()
    {
        if (m_closeOnCancelButton)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                
                gameObject.SetActive(false);

                if (m_unfreezePlayer)
                    m_pCon.PlayerFreeze(false);
                if (m_disableParent)
                    transform.parent.gameObject.SetActive(false);

                if (m_toEnableOnCancel != null)
                {
                    m_toEnableOnCancel.gameObject.SetActive(true);
                    SetSelectedButton(m_toEnableOnCancel);
                } else
                {
                    Scr_global_canvas.MainEventSystem.SetSelectedGameObject(null);
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
        StartCoroutine(WaitForButtonSelect(panel));
    }

    private IEnumerator WaitForButtonSelect(Scr_ui_panel panel)
    {
        Scr_global_canvas.MainEventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
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
