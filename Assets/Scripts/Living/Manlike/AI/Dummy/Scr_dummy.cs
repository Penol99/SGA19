using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Scr_dummy : MonoBehaviour, IGoap
{
    
    public float m_targetRangeLimit, m_targetRangeMin;

    [HideInInspector]
    public Scr_combat_attack_melee m_combat;
    [HideInInspector]
    public NavMeshAgent m_navAgent;
    [HideInInspector]
    public Scr_manlike_input m_manInput;
    [HideInInspector]
    public Scr_manlike_animation m_manAnim;
    [HideInInspector]
    public Scr_manlike_find_target m_findTarget;
    [HideInInspector]
    public Scr_living_stats m_stats;
    [HideInInspector]
    public Vector3 m_startPos;
    [HideInInspector]
    public GameObject m_spawnObj;

    private float m_velocity;
    private float m_currentTargetRange;

    private bool m_targetLost = false;    

    public bool TargetLost { get => m_targetLost; set => m_targetLost = value; }
    public bool HasHitTarget { get => m_combat.m_weapon.HasHit; }

    void Awake()
    {
        m_combat = GetComponent<Scr_combat_attack_melee>();
        m_startPos = transform.position;
        m_spawnObj = new GameObject(gameObject.name + " SPAWN_OBJECT");
        m_spawnObj.transform.position = m_startPos;
        m_stats = GetComponent<Scr_living_stats>();
        m_findTarget = GetComponent<Scr_manlike_find_target>();
        m_manInput = GetComponent<Scr_manlike_input>();
        m_manAnim = GetComponent<Scr_manlike_animation>();
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.updatePosition = false;

        
    }

    void LateUpdate()
    {
        m_manInput.MoveVelocity = m_velocity;
        m_navAgent.speed = m_manInput.CurrentMoveSpeed;
        Stamina();
    }

    private void Stamina()
    {
        

        bool inRun = m_manInput.MoveVelocity > m_manInput.m_walkSpeed;
        int notInAttack = Scr_convert.ToInt(!m_manAnim.LightAttackEnd && !m_manAnim.LightAttackStart);
        m_stats.AddStamina(m_manInput.m_staminaRunGain * Time.deltaTime * Scr_convert.ToInt(!inRun) * notInAttack);
        m_stats.SubStamina(m_manInput.m_staminaRunLoss * Time.deltaTime * Scr_convert.ToInt(inRun));
        // Clamp movespeed to walkspeed if stamina is 0 so that it cant run with insufficent stamina
        if (m_stats.StatStamina <= 0)
            m_manInput.CurrentMoveSpeed = Mathf.Clamp(m_manInput.CurrentMoveSpeed, 0, m_manInput.m_walkSpeed);
    }

    public void ChangeMoveSpeed(float speed)
    {
        m_manInput.CurrentMoveSpeed = speed;
    }


    public void actionsFinished()
    {

    }

    public HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("targetKilled", !TargetLost));
        goal.Add(new KeyValuePair<string, object>("returnToSpawn", TargetLost));

        return goal;
    }

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("targetKilled", false));
        worldData.Add(new KeyValuePair<string, object>("atTarget", false));
        worldData.Add(new KeyValuePair<string, object>("attackTarget", false));
        worldData.Add(new KeyValuePair<string, object>("returnToSpawn", false));
        worldData.Add(new KeyValuePair<string, object>("targetLost", TargetLost));


        return worldData;

    }

    public bool moveAgent(GoapAction nextAction)
    {

        m_velocity = Mathf.Sqrt(Mathf.Pow(m_navAgent.velocity.x, 2) + Mathf.Pow(m_navAgent.velocity.z, 2));
        Vector3 positionUpdate = new Vector3(transform.position.x, m_navAgent.nextPosition.y, transform.position.z);
        m_navAgent.nextPosition = positionUpdate;
        transform.position = positionUpdate;
        transform.rotation = m_navAgent.transform.rotation;

        float dist = (transform.position - nextAction.target.transform.position).sqrMagnitude;

        if (dist < Mathf.Pow(m_targetRangeLimit,2))
        {
            m_navAgent.SetDestination(nextAction.target.transform.position);
        }

        bool moveWithoutRange = nextAction.moveWithoutRange();

        if (dist < Mathf.Pow(m_targetRangeMin,2))
        {

            m_velocity = 0f;
            nextAction.setInRange(!moveWithoutRange);
            return true;
        }  else
        {
            return false;
        }
    }

    public void planAborted(GoapAction aborter)
    {
        m_manInput.CurrentMoveSpeed = 0f;
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        
    }

    
}
