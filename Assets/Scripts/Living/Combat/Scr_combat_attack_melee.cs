using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_combat_attack_melee : MonoBehaviour
{
    public Transform m_handBone;
    public Scr_combat_weapon m_weapon;
    [HideInInspector]
    public bool m_weaponEquipped;

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

    public void Unequip()
    {
        m_weaponEquipped = false;
        m_weapon.gameObject.SetActive(false);
    }

    public void Equip()
    {
        m_weaponEquipped = true;
        m_weapon.gameObject.SetActive(true);
    }
}
