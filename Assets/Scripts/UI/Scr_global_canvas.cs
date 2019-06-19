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
    public GameObject m_bedPanel;
    public GameObject[] m_overlayAnimations;

    public static EventSystem MainEventSystem;
    public static GameObject PlayerStatsPanel;
    public static GameObject PauseMenuPanel;
    public static GameObject DialogueBox;
    public static GameObject ShrineWindow;
    public static GameObject BedPanel;
    public static GameObject[] OverlayAnimations;
    public static bool PauseActive;
    public static bool DialogueActive;
    public static bool ShrineActive;
    public static bool BedActive;

    

    // Start is called before the first frame update
    void Awake()
    {
        MainEventSystem = FindObjectOfType<EventSystem>();
        PlayerStatsPanel = m_playerStatsPanel;
        PauseMenuPanel = m_pauseMenuPanel;
        DialogueBox = m_dialogueBox;
        ShrineWindow = m_shrineWindow;
        BedPanel = m_bedPanel;
        OverlayAnimations = m_overlayAnimations;
    }

    // Update is called once per frame
    void Update()
    {
        DisablePanelOverride();
    }

    private bool ObjectArrayActive(GameObject[] objects)
    {
        bool isActive = false;
        foreach (var obj in objects)
        {
            if (obj.activeInHierarchy)
                isActive = true;
        }

        return isActive;
    }

    private void DisablePanelOverride()
    {
        PauseActive = PauseMenuPanel.activeInHierarchy;
        DialogueActive = DialogueBox.activeInHierarchy;
        ShrineActive = ShrineWindow.activeInHierarchy;
        BedActive = BedPanel.activeInHierarchy;


        Scr_global_pause.CanPause = !DialogueActive && !ShrineActive && !BedActive && !ObjectArrayActive(OverlayAnimations);
        Scr_interact_dialogue.CanStartDialogue = !PauseActive && !ShrineActive;
        Scr_interact_bed.CanGoToBed = !PauseActive && !DialogueActive && !BedActive;
        
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
