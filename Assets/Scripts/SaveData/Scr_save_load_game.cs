using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_save_load_game : MonoBehaviour
{
    [HideInInspector]
    public Scr_living_stats m_player;

    private Scr_player_rpg_stats m_playerRPG;
    private Scr_living_fall_damage m_playerFallDmg;


    private void Awake()
    {
        m_player = FindObjectOfType<Scr_player_controller>().GetComponent<Scr_living_stats>();
        if (PlayerPrefs.GetInt("Continue") == 1)
        {
            // Loads game if continue was selected in the title screen.
            LoadGame();
            PlayerPrefs.SetInt("Continue", 0);
        }
    }


    public void SaveGame()
    {
        m_playerRPG = m_player.GetComponent<Scr_player_rpg_stats>();
        m_playerFallDmg = m_player.GetComponent<Scr_living_fall_damage>();
        
        
        if (m_playerFallDmg.OnGround)
        {
            Scr_save_data.SavePlayer(m_player,m_playerRPG);
            PlayerPrefs.SetInt("SceneIndex", m_player.SceneIndex);
        } else
        {
            Debug.LogWarning("PLAYER MUST BE GROUNDED TO SAVE!");
        }
    }
    public void LoadGame()
    {
        Scr_data_player playerData = Scr_save_data.LoadPlayer();
        m_playerRPG = m_player.GetComponent<Scr_player_rpg_stats>();

        m_player.StatHealth = playerData.m_health;
        m_player.HealthLimit = playerData.m_healthLimit;
        m_player.StatStamina = playerData.m_stamina;
        m_player.StaminaLimit = playerData.m_staminaLimit;

        m_playerRPG.m_money = playerData.m_money;
        m_playerRPG.m_healthLevel = playerData.m_healthLvl;
        m_playerRPG.m_staminaLevel = playerData.m_staminaLvl;
        m_playerRPG.m_agilityLevel = playerData.m_agilityLvl;
        m_playerRPG.m_meleeDmgLevel = playerData.m_meleeDmgLvl;
        m_playerRPG.m_meleeSpdLevel = playerData.m_meleeSpdLvl;
        m_playerRPG.m_vanquishDmgLevel = playerData.m_vanquishDmgLvl;

        Vector3 playerPos;
        // Right now it doesnt handle if the player hasnt been to a shrine, like in the starting area
        if (PlayerPrefs.GetInt("PlayerDied") == 1)
        {
            playerPos.x = PlayerPrefs.GetFloat("ShrinePosX");
            playerPos.y = PlayerPrefs.GetFloat("ShrinePosY");
            playerPos.z = PlayerPrefs.GetFloat("ShrinePosZ");
        } else
        {
            playerPos.x = playerData.position[0];
            playerPos.y = playerData.position[1];
            playerPos.z = playerData.position[2];
        }
        PlayerPrefs.SetInt("PlayerDied", 0);



        m_player.transform.position = playerPos;
    }

}
