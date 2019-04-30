using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_dummy_attack : GoapAction
{
    private Scr_dummy m_dum;
    private bool m_attacked = false;
    private bool m_canTrigger = true;
    private bool m_hasHitDuringAttack = false; // will stay true after hit on target until it doesnt hit a target
    private bool m_missedTarget;

    private void Start()
    {
        m_dum = GetComponent<Scr_dummy>();
        
    }

    public GA_dummy_attack()
    {
        
        addEffect("attackTarget", true);
        addPrecondition("attackTarget", false);
        addPrecondition("atTarget", true);
        addPrecondition("targetKilled", false);

        cost = 25f;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = m_dum.m_findTarget.GetTargetInRange(m_dum.m_targetRangeLimit);
        m_canTrigger = true;
        m_missedTarget = false;
        m_hasHitDuringAttack = false;
        return !m_dum.m_findTarget.TargetNotAlive(target);
    }

    public override bool isDone()
    {
        return m_attacked;
    }

    public override bool perform(GameObject agent)
    {

        bool targetDead = m_dum.m_findTarget.TargetNotAlive(target);
        bool targetInHitRange = m_dum.m_findTarget.IsTargetInRange(gameObject, target, m_dum.m_targetRangeMin);
        bool hasStamina = m_dum.m_stats.StatStamina > 0;
        bool notInAnimation = !m_dum.m_manAnim.LightAttackStart;
        bool inWalkAnimation = m_dum.m_manAnim.IdleWalkRun;
        m_attacked = m_dum.HasHitTarget;

        bool attackTrigger = hasStamina && notInAnimation && targetInHitRange;
        TriggerAttack(attackTrigger);
        
        if (m_attacked || !targetInHitRange)
        {
            m_dum.m_manAnim.ResetLightAttack();
        }

        if (!m_dum.HasHitTarget)
        {   // if it has missed 
            m_missedTarget = true;
        } else
        {
            m_missedTarget = false;
        }

        return targetInHitRange && !inWalkAnimation && !m_missedTarget;
    }

    private void TriggerAttack(bool trigger)
    {
        // MAKES SENSE CAUSE YOU WOULD ONLY HIT AGAIN IF YOU ACTUALLY GOT THE TARGET BUT THIS WILL ALSO MAKE IT IMPOSSIBLE TO HIT IF YOUVE MISSED HMMMM
        if (m_dum.HasHitTarget)
        {
            
            m_canTrigger = true;
        }
        if (trigger)
        {
            if (m_canTrigger)
            {
                m_dum.m_manInput.R1Trigger = true;
                m_canTrigger = false;
            }
        }
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override bool moveWithoutRange()
    {
        return false;
    }

    public override void reset()
    {
        m_attacked = false;
        target = null;
    }

}
