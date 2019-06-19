using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scr_ui_shrine_new_stats : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_levelText;
    public TMPro.TextMeshProUGUI m_healthText;
    public TMPro.TextMeshProUGUI m_staminaText;
    

    private Scr_ui_level_up m_levelUp;
    private Scr_player_rpg_stats m_rpgStats;
    private Scr_living_stats m_playerStats;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_levelUp = GetComponentInParent<Scr_ui_level_up>();
        m_rpgStats = FindObjectOfType<Scr_player_rpg_stats>();
        m_playerStats = m_rpgStats.GetComponent<Scr_living_stats>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatChange();
    }

    private void CheckStatChange()
    {
        if (m_levelUp.m_modifiedLevel-m_rpgStats.CurrentLevel() != 0) // If stats have been modified in the levelup panel
        {
            DisplayStatChange();
        } else
        {
            DisplayNormalStats();
        }
    }

    private void DisplayStatChange()
    {
        m_levelText.text = ("Level: " + m_rpgStats.CurrentLevel() + " > " + m_levelUp.m_modifiedLevel);
        m_healthText.text = ("Health: " + m_playerStats.HealthLimit + " > " + m_rpgStats.CalculateHealth(m_levelUp.modHealthLvl));
        m_staminaText.text = ("Stamina: " + m_playerStats.StaminaLimit + " > " + m_rpgStats.CalculateStamina(m_levelUp.modStaminaLvl));
    }

    private void DisplayNormalStats()
    {
        m_levelText.text = ("Level: " + m_rpgStats.CurrentLevel());
        m_healthText.text = ("Health: " + m_playerStats.HealthLimit);
        m_staminaText.text = ("Stamina: " + m_playerStats.StaminaLimit);
    }
}
