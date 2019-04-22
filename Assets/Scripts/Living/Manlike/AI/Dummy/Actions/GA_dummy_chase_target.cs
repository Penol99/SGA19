using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_dummy_chase_target : GoapAction
{
    private Scr_dummy m_dum;

    private bool m_atTarget;

    public GA_dummy_chase_target()
    {
        addEffect("atTarget", true);
        addPrecondition("targetLost", false);
        cost = 25;

    }

    private void Start()
    {
        m_dum = GetComponent<Scr_dummy>();
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = m_dum.m_findTarget.GetTargetInRange(m_dum.m_targetRangeLimit);
        return !m_dum.m_findTarget.TargetNotAlive(target);
    }

    public override bool isDone()
    {
        return m_atTarget;
    }

    public override bool perform(GameObject agent)
    {
        
        m_atTarget = m_dum.m_findTarget.IsTargetInRange(gameObject, target, m_dum.m_targetRangeMin);
        m_dum.m_manInput.CurrentMoveSpeed = m_dum.m_manInput.m_walkSpeed * System.Convert.ToInt16(!m_atTarget);
        bool hasTarget = !m_dum.m_findTarget.TargetNotAlive(target);
        m_dum.TargetLost = !hasTarget;
        return hasTarget;
    }

    public override bool requiresInRange()
    {
        
        return false;
    }

    public override bool moveWithoutRange()
    {
        return true;
    }

    public override void reset()
    {
        target = null;
        m_atTarget = false;
        
    }

    
}
