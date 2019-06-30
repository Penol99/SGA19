using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum GoldBuddhaState
{
    IDLE,
    ATTACK
}
public class Scr_living_goldbuddha_controller : MonoBehaviour, IHurt
{

    
    public Vector3 m_idleAreaSize;    
    public float m_targetMaxRange = 30f;

    private GoldBuddhaState m_state;
    private Scr_pool_money m_moneyPool;
    private Scr_living_stats m_stats;
    private NavMeshAgent m_navAgent;
    private GameObject m_target;
    private Animator m_anim;
    private bool m_jumpSim;
    private bool m_idleWalk;
    private bool m_randCountStarted;
    private float m_jumpSimTime,m_jumpSimInterval = 1f;
    private float m_randCountTime;
    private float m_randTimeLimit;
    private float m_randCountMin = .5f;
    private float m_randCountMax = 4f;
    private Vector3 m_startPos;
    private bool m_gameStarted;

    


    // Start is called before the first frame update
    void Start()
    {
        m_gameStarted = true;
        m_startPos = transform.position;
        m_moneyPool = FindObjectOfType<Scr_pool_money>();
        m_anim = GetComponent<Animator>();
        m_stats = GetComponent<Scr_living_stats>();
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpSimulation();

        

        switch (m_state)
        {
            case GoldBuddhaState.IDLE:
                Idle();
                break;
            case GoldBuddhaState.ATTACK:
                Attack();
                break;
            default:
                break;
        }


    }
    private void Attack()
    {
        if (m_jumpSim && m_target != null)
            m_anim.SetTrigger("trigger_jump");
    }


    public void Idle()
    {
        if (!m_randCountStarted)
        {
            m_randTimeLimit = Random.Range(m_randCountMin, m_randCountMax);
            m_randCountStarted = true;
        }
        else
        {
            if (m_randCountTime < m_randTimeLimit)
            {
                m_randCountTime += Time.deltaTime;
            } else
            {
                m_idleWalk = true;
                m_randCountStarted = false;
                m_randCountTime = 0f;
            }
        }

        if (m_idleWalk)
        {
            m_anim.SetTrigger("trigger_idle");
            m_navAgent.isStopped = false;
            float randomPosX = Random.Range(m_startPos.x - (m_idleAreaSize.x / 2), m_startPos.x + (m_idleAreaSize.x / 2));
            float randomPosY = Random.Range(m_startPos.y - (m_idleAreaSize.y / 2), m_startPos.y + (m_idleAreaSize.y / 2));
            float randomPosZ = Random.Range(m_startPos.z - (m_idleAreaSize.z / 2), m_startPos.z + (m_idleAreaSize.z / 2));

            Vector3 randomIdlePos = new Vector3(randomPosX, randomPosY, randomPosZ);
            m_navAgent.SetDestination(randomIdlePos);
            m_idleWalk = false;
        }
    }

    public void Move()
    {
        
            m_navAgent.isStopped = false;
            float randomLimit = 4f;
            float randomX = Random.Range(-randomLimit, randomLimit);
            float randomZ = Random.Range(-randomLimit, randomLimit);
            randomX += m_target.transform.position.x;
            randomZ += m_target.transform.position.z;

            Vector3 movePosition = new Vector3(randomX, m_target.transform.position.y, randomZ);
            m_navAgent.SetDestination(movePosition);
       
    }

    public void StopMove()
    {
        m_navAgent.isStopped = true;
    }

    public void Hurt(float damage, GameObject damager)
    {
        m_state = GoldBuddhaState.ATTACK;
        m_moneyPool.DropMoney(gameObject, 1, 5);
        m_target = damager;
        m_stats.SubHealth(damage);
    }

    private void JumpSimulation()
    {
        if (m_jumpSimTime < m_jumpSimInterval)
        {
            m_jumpSim = false;
            m_jumpSimTime += Time.deltaTime;
        }
        else
        {
            m_jumpSim = true;
            m_jumpSimTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 drawPos = m_gameStarted ? m_startPos : transform.position;
        Gizmos.DrawWireCube(drawPos, m_idleAreaSize);
    }


}
