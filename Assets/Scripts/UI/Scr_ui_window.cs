using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ui_window : MonoBehaviour
{
    public Scr_ui_panel[] m_panels;

    private void OnEnable()
    {
        m_panels[0].gameObject.SetActive(true);
        m_panels[0].SetSelectedButton(m_panels[0]);

    }

    private void OnDisable()
    {
        foreach (var p in m_panels)
        {
            p.gameObject.SetActive(false);
        }
    }
}
