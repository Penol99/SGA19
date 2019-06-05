using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_player_rpg_stats : MonoBehaviour
{
    public int m_money;

    public int m_healthLevel = 1;
    public int m_staminaLevel = 1;
    public int m_agilityLevel = 1;
    public int m_meleeDmgLevel = 1;
    public int m_meleeSpdLevel = 1;
    public int m_vanquishDmgLevel = 1;

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
        
}
