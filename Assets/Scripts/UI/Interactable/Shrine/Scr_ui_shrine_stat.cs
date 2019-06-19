using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum RPG_STATS
{
    Health,
    Stamina,
    Agility,
    MeleeDMG,
    MeleeSPD,
    VanquishDMG
}
public class Scr_ui_shrine_stat : MonoBehaviour
{ 
    public RPG_STATS m_statType;
    public TMPro.TextMeshProUGUI m_levelText;
    private EventSystem m_eventSys;
    private Scr_player_rpg_stats m_rpgStats;
    private Scr_ui_level_up m_levelUpPanel;
    private int m_modifiedLevel;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_eventSys = Scr_global_canvas.MainEventSystem;
        m_rpgStats = FindObjectOfType<Scr_player_rpg_stats>();
        m_modifiedLevel = GetCurrentStat(m_statType);
        m_levelUpPanel = FindObjectOfType<Scr_ui_level_up>();
        m_levelText.text = GetCurrentStat(m_statType).ToString();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ModifyLevel();
    }

    private void ModifyLevel()
    {
        if (m_eventSys.currentSelectedGameObject.Equals(gameObject))
        {
            if (m_levelUpPanel.m_modifiedMoney >= m_rpgStats.MoneyForNextLevel(m_levelUpPanel.m_modifiedLevel))
            {
                if (Scr_modded_input.LStickRight)
                    AddLevel();
            }
            if (GetCurrentStat(m_statType) < m_modifiedLevel)
            {
                if (Scr_modded_input.LStickLeft)
                {
                    RemoveLevel();
                }
            }
        }
        m_levelText.text = m_modifiedLevel.ToString();
    }

    private void AddModifyToLevelUp()
    {
        switch (m_statType)
        {
            case RPG_STATS.Health:
                m_levelUpPanel.modHealthLvl = m_modifiedLevel;
                break;
            case RPG_STATS.Stamina:
                m_levelUpPanel.modStaminaLvl = m_modifiedLevel;
                break;
            case RPG_STATS.Agility:
                m_levelUpPanel.modAgilityLvl = m_modifiedLevel;
                break;
            case RPG_STATS.MeleeDMG:
                m_levelUpPanel.modMeleeDMGLvl = m_modifiedLevel;
                break;
            case RPG_STATS.MeleeSPD:
                m_levelUpPanel.modMeleeSPDLvl = m_modifiedLevel;
                break;
            case RPG_STATS.VanquishDMG:
                m_levelUpPanel.modVanquishDMGLvl = m_modifiedLevel;
                break;
            default:
                break;
        }
    }

    private void AddLevel()
    {
        m_levelUpPanel.m_modifiedMoney -= m_rpgStats.MoneyForNextLevel(m_levelUpPanel.m_modifiedLevel);
        m_levelUpPanel.m_modifiedLevel++;
        m_modifiedLevel++;
        AddModifyToLevelUp();
    }
    private void RemoveLevel()
    {
        m_levelUpPanel.m_modifiedMoney += m_rpgStats.MoneyForNextLevel(m_levelUpPanel.m_modifiedLevel-1);
        m_levelUpPanel.m_modifiedLevel--;
        m_modifiedLevel--;
        AddModifyToLevelUp();
    }

    private int GetCurrentStat(RPG_STATS type)
    {
        int currentLevel = 0;
        switch (type)
        {
            case RPG_STATS.Health:
                currentLevel = m_rpgStats.m_healthLevel;
                break;
            case RPG_STATS.Stamina:
                currentLevel = m_rpgStats.m_staminaLevel;
                break;
            case RPG_STATS.Agility:
                currentLevel = m_rpgStats.m_agilityLevel;
                break;
            case RPG_STATS.MeleeDMG:
                currentLevel = m_rpgStats.m_meleeDmgLevel;
                break;
            case RPG_STATS.MeleeSPD:
                currentLevel = m_rpgStats.m_meleeSpdLevel;
                break;
            case RPG_STATS.VanquishDMG:
                currentLevel = m_rpgStats.m_vanquishDmgLevel;
                break;
            default:
                currentLevel = 696969;
                break;
        }
        return currentLevel;
    }

    public void ApplyModifiedLevel(RPG_STATS type)
    {
        switch (type)
        {
            case RPG_STATS.Health:
                m_rpgStats.m_healthLevel = m_modifiedLevel;
                break;
            case RPG_STATS.Stamina:
                m_rpgStats.m_staminaLevel = m_modifiedLevel;
                break;
            case RPG_STATS.Agility:
                m_rpgStats.m_agilityLevel = m_modifiedLevel;
                break;
            case RPG_STATS.MeleeDMG:
                m_rpgStats.m_meleeDmgLevel = m_modifiedLevel;
                break;
            case RPG_STATS.MeleeSPD:
                m_rpgStats.m_meleeSpdLevel = m_modifiedLevel;
                break;
            case RPG_STATS.VanquishDMG:
                m_rpgStats.m_vanquishDmgLevel = m_modifiedLevel;
                break;
            default:
                break;
        }
        m_modifiedLevel = GetCurrentStat(m_statType);
    }
}
