using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Scr_global_canvas : MonoBehaviour
{
    
    public GameObject m_playerStatsPanel;
    public GameObject m_pauseMenuPanel;
    public GameObject m_dialogueBox;
    public GameObject m_shrineWindow;

    public static EventSystem MainEventSystem;
    public static GameObject PlayerStatsPanel;
    public static GameObject PauseMenuPanel;
    public static GameObject DialogueBox;
    public static GameObject ShrineWindow;

    

    // Start is called before the first frame update
    void Awake()
    {
        MainEventSystem = FindObjectOfType<EventSystem>();
        PlayerStatsPanel = m_playerStatsPanel;
        PauseMenuPanel = m_pauseMenuPanel;
        DialogueBox = m_dialogueBox;
        ShrineWindow = m_shrineWindow;
    }

    // Update is called once per frame
    void Update()
    {
        DisablePanelOverride();
    }

    private void DisablePanelOverride()
    {
        Scr_global_pause.CanPause = !DialogueBox.activeInHierarchy || !ShrineWindow.activeInHierarchy;
        Scr_interact_dialogue.CanStartDialogue = !PauseMenuPanel.activeInHierarchy;
    }

    public static void SetPanelActive(GameObject panel, bool value, bool setSelectedObject)
    {
        panel.SetActive(value);

        if (value && setSelectedObject)
        {
            GameObject firstSelected = panel.GetComponent<Scr_ui_panel>().m_firstSelectedObject;
            MainEventSystem.SetSelectedGameObject(firstSelected);
        } else
        {
            MainEventSystem.SetSelectedGameObject(null);
        }
    }

    
}
