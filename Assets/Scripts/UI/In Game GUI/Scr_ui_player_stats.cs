using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_ui_player_stats : MonoBehaviour
{
    public float m_contentBarHorSize = 100f;

    private Scr_living_stats m_playerStats;
    public Slider m_healthBar,m_staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        m_playerStats = FindObjectOfType<Scr_player_controller>().GetComponent<Scr_living_stats>();
    }

    // Update is called once per frame
    void Update()
    {
        m_staminaBar.value = m_playerStats.StatStamina / m_playerStats.StaminaLimit;
        m_healthBar.value = m_playerStats.StatHealth / m_playerStats.HealthLimit;
        RectTransform hbar = m_healthBar.GetComponent<RectTransform>();
        RectTransform sbar = m_staminaBar.GetComponent<RectTransform>();
        hbar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_playerStats.HealthLimit * (610 / m_contentBarHorSize));
        sbar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_playerStats.StaminaLimit * (610 / m_contentBarHorSize));

    }
}
