using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scr_data_player
{
    // Later i need to add rpg elements 

    public int m_sceneIndex;
    public float m_health;
    public float m_healthLimit;
    public float m_stamina;
    public float m_staminaLimit;
    public float[] position;

    public Scr_data_player (Scr_living_stats player)
    {
        m_health = player.StatHealth;
        m_stamina = player.StatStamina;
        m_healthLimit = player.HealthLimit;
        m_staminaLimit = player.StaminaLimit;
        m_sceneIndex = player.SceneIndex;


        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
