using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_combat_weapon : MonoBehaviour
{

    public string m_weaponName;
    [Header("Stats")]
    public float m_equipLoad;
    public float m_staminaLoss;
    [Header("Hitboxes")]
    public LayerMask m_targetLayer;
    public List<Scr_combat_hitbox> m_hitboxes;

    private bool m_hasHit; // true after hitbox has ended if directHasHit has been true once;
    private bool m_directHasHit; // true instantly when hitbox has hit;
    private HashSet<Collider> m_collidersHit = new HashSet<Collider>();
    private bool m_enableHitboxes;


    public bool HasHit { get => m_hasHit; set => m_hasHit = value; }
    public bool DirectHasHit { get => m_directHasHit; set => m_directHasHit = value; }

    public void EnableHitboxes(bool state, Scr_living_stats stats)
    {
        m_enableHitboxes = state;
        if (!state)
        {
            HasHit = DirectHasHit; // Maybe i should add a timer so that HasHit reverts back to false so its not constantly false waiting for next attack..hmm
            DirectHasHit = false;
            m_collidersHit.Clear();
            m_collidersHit.TrimExcess();
        } else
        {
            stats.SubStamina(m_staminaLoss); // stamina loss from using weapon
        }
    }

    private void Update()
    {
        if (m_enableHitboxes)
        {
            
            foreach (var hitbox in m_hitboxes)
            {
                hitbox.DrawHitbox(m_targetLayer);
            }
        }
    }

}
