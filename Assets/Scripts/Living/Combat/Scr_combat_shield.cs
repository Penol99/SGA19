using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_combat_shield : MonoBehaviour, IHurt
{
    public float m_defense = 10f;


    public Scr_manlike_animation m_manAnim; // This needs to be set when the shield is equipped.
    private BoxCollider m_collider;
    private Scr_living_stats m_stats;   // This needs to be set when the shield is equipped.

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<BoxCollider>();

        if (m_manAnim != null)
            m_stats = m_manAnim.GetComponent<Scr_living_stats>();
    }



    // Update is called once per frame
    void Update()
    {
        if (m_manAnim != null)
            m_collider.enabled = m_manAnim.Shield;
    }

    public void Hurt(float damage, GameObject damager)
    {

    }

}
