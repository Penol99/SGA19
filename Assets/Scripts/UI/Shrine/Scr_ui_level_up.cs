using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ui_level_up : MonoBehaviour
{
    [HideInInspector]
    public int m_modifiedMoney, m_modifiedLevel;

    private Scr_ui_shrine_stat[] m_shrineStats;
    private Scr_player_rpg_stats m_rpgStats;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_rpgStats = FindObjectOfType<Scr_player_rpg_stats>();
        m_shrineStats = GetComponentsInChildren<Scr_ui_shrine_stat>();
        m_modifiedMoney = m_rpgStats.m_money;
        m_modifiedLevel = m_rpgStats.CurrentLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyStats()
    {
        foreach (var stat in m_shrineStats)
        {
            stat.ApplyModifiedLevel(stat.m_statType);
        }
    }
}
