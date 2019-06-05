using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_equipment_pool : MonoBehaviour
{


    
    private static List<Scr_combat_weapon> m_weapons = new List<Scr_combat_weapon>();
    private static List<Scr_combat_shield> m_shields = new List<Scr_combat_shield>();

    public static List<Scr_combat_weapon> m_playerWeapons = new List<Scr_combat_weapon>();


    // Start is called before the first frame update
    void Start()
    {

        m_weapons.AddRange(GetComponentsInChildren<Scr_combat_weapon>());
        m_shields.AddRange(GetComponentsInChildren<Scr_combat_shield>());
        AddWeaponToInventory("Blue Sword");
    }

    public void AddWeaponToInventory(string weaponName)
    {
        foreach (var weapon in m_weapons)
        {
            if (weapon.m_weaponName.Equals(weaponName))
            {
                m_playerWeapons.Add(weapon);
                return;
            }   
        }

        Debug.LogWarning("WEAPON DOESNT EXIST IN WEAPON POOL");
    }

}
