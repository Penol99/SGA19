using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_combat_attack_melee : MonoBehaviour
{
    public Scr_combat_weapon m_weapon;

    private Scr_living_stats m_stats;

    private void Start()
    {
        m_stats = GetComponent<Scr_living_stats>();
    }

    public void BeginAttack()
    {
        m_weapon.EnableHitboxes(true,m_stats);
    }

    public void EndAttack()
    {
        m_weapon.EnableHitboxes(false,m_stats);
    }
}
