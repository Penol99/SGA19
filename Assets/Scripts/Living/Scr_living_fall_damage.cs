using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scr_living_stats))]
public class Scr_living_fall_damage : MonoBehaviour
{
    private Scr_living_stats m_stats;
    private float m_fallDmgMultiplier = 120f;
    private float m_rayDownDist = .5f;
    private float m_minimumFallTime = 2f;
    private float m_fallTimer = 0f;
    private bool m_onGround;



    // Start is called before the first frame update
    void Start()
    {
        m_stats = GetComponent<Scr_living_stats>();
    }

    private KeyValuePair<bool, float> FallTimer(float minTime)
    {
        float timeOnImpact = m_fallTimer;
        bool inDamageRange = timeOnImpact > minTime;
        KeyValuePair<bool, float> fallTime = new KeyValuePair<bool, float>(inDamageRange, timeOnImpact);
        m_onGround = Physics.Raycast(transform.position, -transform.up, m_rayDownDist);
        m_fallTimer = (m_fallTimer + Time.deltaTime) * System.Convert.ToInt16(!m_onGround);
        return fallTime;
    }

    private float FallDamage(float damageMultiplier, KeyValuePair<bool, float> fallTime)
    {
        float fallDamage = damageMultiplier * fallTime.Value * System.Convert.ToInt16(fallTime.Key);
        return fallDamage * System.Convert.ToInt16(m_onGround);
    }

    // Update is called once per frame
    void Update()
    {
        m_stats.SubHealth(FallDamage(m_fallDmgMultiplier, FallTimer(m_minimumFallTime)));
    }
}
