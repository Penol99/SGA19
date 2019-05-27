using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_save_load_game : MonoBehaviour
{
    public Scr_living_stats m_player;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        Debug.Log(PlayerPrefs.GetInt("SceneIndex"));
    }
    public void LoadGame()
    {
        Scr_data_player playerData = Scr_save_data.LoadPlayer();

        m_player.StatHealth = playerData.m_health;
        m_player.HealthLimit = playerData.m_healthLimit;
        m_player.StatStamina = playerData.m_stamina;
        m_player.StaminaLimit = playerData.m_staminaLimit;

        Vector3 playerPos;
        playerPos.x = playerData.position[0];
        playerPos.y = playerData.position[1];
        playerPos.z = playerData.position[2];

        m_player.transform.position = playerPos;
    }

}
