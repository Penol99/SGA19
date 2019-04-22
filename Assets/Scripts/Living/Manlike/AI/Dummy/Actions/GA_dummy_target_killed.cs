using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_dummy_target_killed : GoapAction
{

    Scr_dummy m_dum;

    public GA_dummy_target_killed()
    {
        addEffect("targetKilled", true);
        addPrecondition("attackTarget", true);
        addPrecondition("targetKilled", false);
        cost = 10;
    }

    private void Start()
    {
        m_dum = GetComponent<Scr_dummy>();
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = m_dum.m_findTarget.GetTargetInRange(m_dum.m_targetRangeLimit);
  
        return target != null;
    }

    public override bool isDone()
    {
        bool isDead = m_dum.m_findTarget.TargetNotAlive(target);
        m_dum.TargetLost = isDead;
        return isDead;
    }

    public override bool perform(GameObject agent)
    {

        return m_dum.m_findTarget.TargetNotAlive(target);
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
        
    }
}
