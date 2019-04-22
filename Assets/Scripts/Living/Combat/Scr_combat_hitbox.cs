using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_combat_hitbox : MonoBehaviour
{
    public bool m_hasWeapon;

    public float m_damage;

    private GameObject m_rootParent;
    private Scr_combat_weapon m_weapon;
    private float m_cantHurtDelayTime = 0.5f;
    private bool m_drawGizmos;
    private BoxCollider m_collider;
    private HashSet<IHurt> m_targetsHit = new HashSet<IHurt>();
    private HashSet<Collider> m_collidersHit = new HashSet<Collider>();



    void Start()
    {
        if (m_hasWeapon)
            m_weapon = GetComponentInParent<Scr_combat_weapon>();

        m_rootParent = transform.root.gameObject;
        m_collider = GetComponent<BoxCollider>();
    }

    public void DrawHitbox(LayerMask layer)
    {
        m_drawGizmos = true;
        Collider[] collidersHit = Physics.OverlapBox(transform.position, transform.lossyScale / 2, m_collider.transform.rotation, layer);
        m_collidersHit.UnionWith(collidersHit);
        foreach (var col in m_collidersHit)
        {
            if (col.GetComponent<IHurt>() != null)
            {
                HitTarget(col.GetComponent<IHurt>(), m_damage);
            }
        }

        m_collidersHit.Clear();
        m_collidersHit.TrimExcess();
    }
    // REMINDER FOR WHEN I GET BACK TO THIS, THIS CODE IS TRIGGERED FOR BOTH THE SHIELD AND THE TARGET, THE SHIELD CLASS HAS THE IHURT INTERFACE
    public void HitTarget(IHurt target,float damage)
    {
        if (!m_targetsHit.Contains(target))
        {
            if (m_hasWeapon)
                m_weapon.DirectHasHit = true;
            target.Hurt(m_damage,m_rootParent);
            m_targetsHit.Add(target);
            StartCoroutine(CantHitDelay(target));
        }
    }

    private IEnumerator CantHitDelay(IHurt target)
    {
        yield return new WaitForSeconds(m_cantHurtDelayTime);

        m_targetsHit.Remove(target);
    }


    private void OnDrawGizmos()
    {
        if (m_drawGizmos)
        {
            Gizmos.color = new Color(255, 0f, 0f, .5f);
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(transform.localPosition,Vector3.one);
            m_drawGizmos = false;
        }

    }
}
