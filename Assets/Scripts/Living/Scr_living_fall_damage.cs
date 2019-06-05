using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scr_living_stats))]
public class Scr_living_fall_damage : MonoBehaviour
{
    public LayerMask m_groundLayer;
    public float m_sphereCheckOffset = 3f;
    public float m_sphereCheckRadius = 0.2f;
    private Scr_living_stats m_stats;
    private float m_fallDmgMultiplier = 40f;
    private float m_rayDownDist = .5f;
    private float m_minimumFallTime = .8f;
    private float m_fallTimer = 0f;   
    private bool m_onGround;

    public bool OnGround { get => m_onGround; set => m_onGround = value; }



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

        /*
        bool rayDF = Physics.Raycast(transform.position+transform.forward/m_rayOffset, -transform.up, m_rayDownDist);
        bool rayDB = Physics.Raycast(transform.position-transform.forward/m_rayOffset, -transform.up, m_rayDownDist);
        bool rayDR = Physics.Raycast(transform.position+transform.right/m_rayOffset, -transform.up, m_rayDownDist);
        bool rayDL = Physics.Raycast(transform.position-transform.right/m_rayOffset, -transform.up, m_rayDownDist);
        bool rayD = Physics.Raycast(transform.position, -transform.up, m_rayDownDist);
        */
        Vector3 sphereOffset = new Vector3(0, m_sphereCheckOffset, 0f);
        OnGround = Physics.CheckSphere(transform.position - sphereOffset, m_sphereCheckRadius,m_groundLayer);//rayDF || rayDB || rayDR || rayDL || rayD;

        m_fallTimer = (m_fallTimer + Time.deltaTime) * System.Convert.ToInt16(!OnGround);
        return fallTime;
    }

    private float FallDamage(float damageMultiplier, KeyValuePair<bool, float> fallTime)
    {
        float fallDamage = damageMultiplier * fallTime.Value * System.Convert.ToInt16(fallTime.Key);
        return fallDamage * System.Convert.ToInt16(OnGround);
    }

    // Update is called once per frame
    void Update()
    {       
        m_stats.SubHealth(FallDamage(m_fallDmgMultiplier, FallTimer(m_minimumFallTime)));       
    }

    private void OnDrawGizmos()
    {
        Vector3 sphereOffset = new Vector3(0, m_sphereCheckOffset, 0f);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position - sphereOffset, m_sphereCheckRadius);
    }
}
