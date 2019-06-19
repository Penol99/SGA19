using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ui_level_up : MonoBehaviour
{
    public int m_modifiedMoney, m_modifiedLevel;

    public int modHealthLvl, modStaminaLvl, modAgilityLvl, modMeleeDMGLvl, modMeleeSPDLvl, modVanquishDMGLvl;

    private Scr_ui_shrine_stat[] m_shrineStats;
    private Scr_player_rpg_stats m_rpgStats;
    private Scr_living_stats m_playerStats;
    private Scr_save_load_game m_saveManager;


    // Start is called before the first frame update
    void OnEnable()
    {
        m_saveManager = FindObjectOfType<Scr_save_load_game>();
        m_rpgStats = FindObjectOfType<Scr_player_rpg_stats>();
        m_playerStats = m_rpgStats.GetComponent<Scr_living_stats>();
        m_shrineStats  = GetComponentsInChildren<Scr_ui_shrine_stat>();
        m_modifiedMoney = m_rpgStats.m_money;
        m_modifiedLevel = m_rpgStats.CurrentLevel();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            ApplyStats();
        }
    }


    public void ApplyStats()
    {
        foreach (var stat in m_shrineStats)
        {
            stat.ApplyModifiedLevel(stat.m_statType);
        }

        m_rpgStats.m_money = m_modifiedMoney;

        m_playerStats.HealthLimit = m_rpgStats.CalculateHealth(modHealthLvl);
        m_playerStats.StaminaLimit = m_rpgStats.CalculateStamina(modStaminaLvl);
        m_playerStats.StatHealth = m_playerStats.HealthLimit;
        m_playerStats.StatStamina = m_playerStats.StaminaLimit;

        m_modifiedLevel = m_rpgStats.CurrentLevel();
        m_modifiedMoney = m_rpgStats.m_money;

        m_saveManager.SaveGame();
        
        
    }
}
