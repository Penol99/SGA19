using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_combat_hitbox : MonoBehaviour
{
    public bool m_displayGizmo;
    public bool m_attachedToAWeapon;
    public float m_damage;
    public Transform m_hitTransform;

    private GameObject m_rootParent;
    private Scr_combat_weapon m_weapon;
    private float m_cantHurtDelayTime = 0.5f;
    private bool m_drawGizmos;
    private HashSet<IHurt> m_targetsHit = new HashSet<IHurt>();
    private HashSet<Collider> m_collidersHit = new HashSet<Collider>();
    private float m_yShrinkValue, m_yShrinkMaxValue;
    private bool m_shrink;
    private float m_maxYLossy;

    private void Update()
    {
        //ShieldShrink();
    }

    void Start()
    {
        if (m_attachedToAWeapon)
            m_weapon = GetComponentInParent<Scr_combat_weapon>();

        m_rootParent = transform.root.gameObject;

        m_yShrinkMaxValue = transform.localScale.y;
        m_maxYLossy = transform.lossyScale.y;
    }

    public void DrawHitbox(LayerMask layer)
    {
        
        m_drawGizmos = true;
        Collider[] collidersHit = Physics.OverlapBox(m_hitTransform.position, m_hitTransform.lossyScale / 2, m_hitTransform.rotation, layer);
        m_collidersHit.UnionWith(collidersHit);


        foreach (var col in m_collidersHit)
        {
            if (col.GetComponent<IHurt>() != null)
            {
                if (col.GetComponent<Scr_combat_shield>() != null)
                {

                    HitTarget(col.GetComponent<IHurt>(), m_damage);
                    break;
                }

                HitTarget(col.GetComponent<IHurt>(), m_damage);
                
            }
        }
        

        m_collidersHit.Clear();
        m_collidersHit.TrimExcess();
    }

    // Shrinks hitbox on shield
    private void ShieldShrink()
    {

        RaycastHit hit;
        //Debug.DrawRay(transform.position, -m_hitTransform.up * transform.lossyScale.y, Color.blue);
        //m_shrink = Physics.Raycast(transform.position, -m_hitTransform.up, transform.lossyScale.y, m_weapon.m_targetLayer);
        
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position - (m_hitTransform.up * m_maxYLossy);
        m_shrink = Physics.Linecast(startPos, endPos, out hit, m_weapon.m_targetLayer);
        Debug.DrawLine(startPos, endPos, Color.blue);
        
        if (m_shrink)
            m_yShrinkValue = hit.distance * transform.localScale.y * 2;
        else
            m_yShrinkValue = m_yShrinkMaxValue;
            
        m_yShrinkValue = Mathf.Clamp(m_yShrinkValue, 0, m_yShrinkMaxValue);
        transform.localScale = new Vector3(transform.localScale.x,m_yShrinkValue, transform.localScale.z); 
        
    }
    // REMINDER FOR WHEN I GET BACK TO THIS, THIS CODE IS TRIGGERED FOR BOTH THE SHIELD AND THE TARGET, THE SHIELD CLASS HAS THE IHURT INTERFACE
    public void HitTarget(IHurt target,float damage)
    {
        if (!m_targetsHit.Contains(target))
        {
            if (m_attachedToAWeapon)
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
        if (m_drawGizmos || m_displayGizmo)
        {
            Gizmos.color = new Color(255, 0f, 0f, .5f);
            Gizmos.matrix = m_hitTransform.localToWorldMatrix;
            Gizmos.DrawCube(m_hitTransform.localPosition,Vector3.one);
            m_drawGizmos = false;
        }

    }
}
