using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_manlike_hurt : MonoBehaviour, IHurt
{
    Scr_manlike_input m_manInput;
    Scr_living_stats m_stats;
    
    void Start()
    {
        m_stats = GetComponent<Scr_living_stats>();
        m_manInput = GetComponent<Scr_manlike_input>();
    }

    public void Hurt(float damage, GameObject damager)
    {
        m_manInput.StunlockTrigger = true;
        m_stats.SubHealth(damage);
    }

}
