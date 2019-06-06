using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_player_rpg_stats : MonoBehaviour
{
    public int m_money = 500;

    public int m_healthLevel = 1;
    public int m_staminaLevel = 1;
    public int m_agilityLevel = 1;
    public int m_meleeDmgLevel = 1;
    public int m_meleeSpdLevel = 1;
    public int m_vanquishDmgLevel = 1;

    private int m_baseHealth = 100;
    private int m_baseStamina = 50;

    private void Start()
    {

    }

    public int MoneyForNextLevel(int currentLevel)
    {
        double calculation = 0.04f * Mathf.Pow(currentLevel+1, 3) + 2.07 * Mathf.Pow(currentLevel+1, 2) + 15.6f * (currentLevel+1) - 19f;

        return Mathf.RoundToInt((float)calculation);
    }

    public int CurrentLevel()
    {
        int level = (m_healthLevel-1)+(m_staminaLevel-1)+(m_agilityLevel-1)+(m_meleeDmgLevel-1)+(m_meleeSpdLevel-1)+(m_vanquishDmgLevel-1)+1;

        return level;
    }

    public int CalculateHealth(int level)
    {
        int hp;
        if (level == 1)
        {
            hp = m_baseHealth;
        } else
        {
            hp = Mathf.RoundToInt(m_baseHealth + Mathf.Pow(m_baseHealth * level, 0.65f));
        }
        return hp;
    }
    public int CalculateStamina(int level)
    {
        int stamina;
        if (level == 1)
        {
            stamina = m_baseHealth;
        }
        else
        {
            stamina = Mathf.RoundToInt(m_baseStamina + Mathf.Pow(m_baseStamina * level, 0.55f));
        }
        return stamina;
    }
        
}
