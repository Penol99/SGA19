using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scr_ui_bed_panel : MonoBehaviour
{
    public GameObject m_sleepTightAnim;
    public TMPro.TextMeshProUGUI dygnText;
    Scr_dygn_manager m_dygnManager;

    // Start is called before the first frame update
    void Start()
    {
        m_dygnManager = FindObjectOfType<Scr_dygn_manager>();
        m_sleepTightAnim.SetActive(false);

        if (m_dygnManager.GetDygnPhase().Equals(DygnPhase.DAY))
            dygnText.text = "Sleep til night?";
        else
            dygnText.text = "Sleep til day?";
    }

    public void GoToBed()
    {
        m_sleepTightAnim.SetActive(true);
        if (m_dygnManager.GetDygnPhase().Equals(DygnPhase.DAY)) // if its Day
        {
            m_dygnManager.SetDygnPhase(DygnPhase.NIGHT);
        } else // If its Night
        {
            m_dygnManager.SetDygnPhase(DygnPhase.DAY);
        }

        Scr_global_canvas.SetPanelActive(gameObject, false, false);
    }
}
