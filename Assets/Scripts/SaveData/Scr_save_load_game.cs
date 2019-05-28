using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_save_load_game : MonoBehaviour
{
    public Scr_living_stats m_player;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("Continue") == 1)
        {
            // Loads game if continue was selected in the title screen.
            LoadGame();
            PlayerPrefs.SetInt("Continue", 0);
        }
    }


    public void SaveGame()
    {
        Scr_save_data.SavePlayer(m_player);
        PlayerPrefs.SetInt("SceneIndex", m_player.SceneIndex);
    }
    public void LoadGame()
    {
        Scr_data_player playerData = Scr_save_data.LoadPlayer();

        m_player.StatHealth = playerData.m_health;
        m_player.HealthLimit = playerData.m_healthLimit;
        m_player.StatStamina = playerData.m_stamina;
        m_player.StaminaLimit = playerData.m_staminaLimit;

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
