using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_equipment_manager : MonoBehaviour
{
    #region Weapons
    public struct Weapon
    {

        public WeaponName m_weaponName;
        public Scr_combat_weapon m_weapon;
        public Transform m_parent;
        public Vector3 m_localPos;
        public Vector3 m_localEulerRot;

        public Weapon(Scr_combat_weapon weapon,Transform parent, Vector3 localPos, Vector3 localEulerRot)
        {            
            m_weapon = weapon;
            m_weaponName = weapon.m_weaponName;
            m_localPos = localPos;
            m_localEulerRot = localEulerRot;
            m_parent = parent;
            m_weapon.transform.parent = parent;
            m_weapon.transform.localPosition = localPos;
            m_weapon.transform.localEulerAngles = localEulerRot;
        }

    }
    public Weapon[] m_stickWeapons;
    #endregion

    public string m_handBoneName;
    public GameObject m_stickParent;
    public Scr_combat_weapon m_equippedWeapon;

    private Vector3 m_weaponLocalPos = new Vector3(-0.00043613f, 0.0005559094f, -0.0003835475f);
    private Vector3 m_weaponLocalEuler = new Vector3(-0.821f, -40.106f, -157.91f);
    private Scr_combat_attack_melee m_player;
    private Scr_combat_weapon[] m_sticks;

    

    // Start is called before the first frame update
    void Awake()
    {
        m_player = FindObjectOfType<Scr_player_controller>().GetComponent<Scr_combat_attack_melee>();
        m_sticks = m_stickParent.GetComponentsInChildren<Scr_combat_weapon>();

        InitWeaponStructs();

        EquipWeapon(WeaponName.STICK_NORMAL);
    }

    private void InitWeaponStructs()
    {
        m_stickWeapons = new Weapon[m_sticks.Length];
        for (int i = 0; i < m_stickWeapons.Length; i++)
        {
            m_stickWeapons[i] = new Weapon(m_sticks[i], m_player.m_handBone, m_weaponLocalPos, m_weaponLocalEuler);
            m_stickWeapons[i].m_weapon.gameObject.SetActive(false); // Deactivate weapon when added to array
        }
    }

    public void EquipWeapon(WeaponName weaponName)
    {
        foreach (var weapon in m_stickWeapons)
        {
            if (weapon.m_weaponName.Equals(weaponName))
            {
                weapon.m_weapon.gameObject.SetActive(true);
                if (m_equippedWeapon != null)
                    m_equippedWeapon.gameObject.SetActive(false);
                m_equippedWeapon = weapon.m_weapon;
                m_player.m_weapon = weapon.m_weapon;
                return;
            }
        }
        Debug.LogWarning("WEAPON NOT FOUND IN WEAPON LIST");
    }

    private void Update()
    {

    }

}
