using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_dummy_return_to_spawn : GoapAction
{
    
    private Scr_dummy m_dum;

    private bool m_atSpawn;

    private void Start()
    {
        m_dum = GetComponent<Scr_dummy>();
        
    }

    public GA_dummy_return_to_spawn()
    {
        addEffect("returnToSpawn", true);
        addPrecondition("targetLost", true);
        cost = 300;
    }
    //CREATE A AT SPAWN ACTION
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = m_dum.m_spawnObj;
        return target != null;
    }

    public override bool isDone()
    {
        m_dum.TargetLost = !m_atSpawn;
        return m_atSpawn;
    }

    public override bool perform(GameObject agent)
    {
        m_dum.m_manInput.CurrentMoveSpeed = m_dum.m_manInput.m_walkSpeed / 2;
        m_atSpawn = m_dum.m_findTarget.IsTargetInRange(gameObject,target,m_dum.m_targetRangeMin);
        bool targetNearby = m_dum.m_findTarget.GetTargetInRange(m_dum.m_targetRangeLimit) != null;
        m_dum.TargetLost = !targetNearby;
        return !targetNearby;
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
        target = m_dum.m_spawnObj;
        m_atSpawn = false;
        
    }
}
