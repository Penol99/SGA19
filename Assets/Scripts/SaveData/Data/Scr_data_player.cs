using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scr_data_player
{

    public int m_money;
    public int m_healthLvl;
    public int m_staminaLvl;
    public int m_agilityLvl;
    public int m_meleeDmgLvl;
    public int m_meleeSpdLvl;
    public int m_vanquishDmgLvl;

    public int m_sceneIndex;
    public float m_health;
    public float m_healthLimit;
    public float m_stamina;
    public float m_staminaLimit;
    public float[] position;

    public Scr_data_player (Scr_living_stats player, Scr_player_rpg_stats playerRPG)
    {
        m_health = player.StatHealth;
        m_stamina = player.StatStamina;
        m_healthLimit = player.HealthLimit;
        m_staminaLimit = player.StaminaLimit;
        m_sceneIndex = player.SceneIndex;

        m_money = playerRPG.m_money;
        m_healthLvl = playerRPG.m_healthLevel;
        m_staminaLvl = playerRPG.m_staminaLevel;
        m_agilityLvl = playerRPG.m_agilityLevel;
        m_meleeDmgLvl = playerRPG.m_meleeDmgLevel;
        m_meleeSpdLvl = playerRPG.m_meleeSpdLevel;
        m_vanquishDmgLvl = playerRPG.m_vanquishDmgLevel;
        


    position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
