using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DygnPhase { DAY, NIGHT}

public class Scr_dygn_manager : MonoBehaviour
{
    

    private static DygnPhase CurrentDygnPhase;
    private static DygnPhase NextDygnPhase;
    private static bool DygnPhaseChanged;
    private static Vector3 CurrentLightRot;

    private Vector3 m_dayLightRot = new Vector3(60, 0, 0);
    private Vector3 m_nightLightRot = new Vector3(220, 0, 0);

    private Scr_save_load_game m_saveManager;
    private Transform m_mainLight;

    


    private void Update()
    {
        m_mainLight.transform.eulerAngles = CurrentLightRot;
    }

    private void Awake()
    {
        m_mainLight = GameObject.Find("Directional Light").transform;

        if (CurrentDygnPhase.Equals(DygnPhase.DAY))
            DayChanges();
        else
            NightChanges();

        if (DygnPhaseChanged)
        {
            // Adjust world changes for the dygnchange here. I might need to add another function here
            m_saveManager = FindObjectOfType<Scr_save_load_game>();
            CurrentDygnPhase = NextDygnPhase;
            StartCoroutine(m_saveManager.DelaySaveGame(.25f));
            DygnPhaseChanged = false;            
        }

    }

    private void DayChanges()
    {
        CurrentLightRot = m_dayLightRot;
    }
    private void NightChanges()
    {
        CurrentLightRot = m_nightLightRot;
    }

    public void SetDygnPhase(DygnPhase phase)
    {
        NextDygnPhase = phase;
        DygnPhaseChanged = true;
        // Restart scene and show some graphic that implies the current dygnphase is being changed
    }

    public void SetDygnPhaseDirectly(DygnPhase phase) // Only used for loading save files, it doesnt actually change the world
    {
        CurrentDygnPhase = phase;
    }

    public DygnPhase GetDygnPhase()
    {
        return CurrentDygnPhase;
    }

}
