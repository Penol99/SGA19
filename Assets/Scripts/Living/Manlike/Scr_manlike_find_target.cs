using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_manlike_find_target : MonoBehaviour
{

    public LayerMask m_targetType;
    private List<GameObject> m_targets = new List<GameObject>(); 
    private Scr_heap_sort m_heap;

    private void Start()
    {
        m_heap = gameObject.AddComponent<Scr_heap_sort>();
        m_targets = Scr_global_lists.LayerMaskToList(m_targetType);
    }


    public GameObject GetTargetInRange(float range)
    {
        List<float> targetDistances = new List<float>();
        List<GameObject> availableTargets = new List<GameObject>();
        if (m_targets.Count > 0)
        {
            foreach (var target in m_targets)
            {
                if (IsTargetInRange(gameObject,target,range))
                {
                    targetDistances.Add((transform.position - target.transform.position).sqrMagnitude);
                    availableTargets.Add(target);
                }
            }
            if (availableTargets.Count > 0)
            {
                m_heap.HeapSortFloat(targetDistances, availableTargets);
                foreach (var t in availableTargets)
                {
                    if (!TargetNotAlive(t))
                        return t;
                }
            }
        }        
        return null;
    }

    public bool IsTargetInRange(Vector3 currentPos, Vector3 targetPos, float range)
    {
        range = range * range;

        return (currentPos - targetPos).sqrMagnitude < range;
    }
    public bool IsTargetInRange(GameObject currentObj, GameObject target, float range)
    {
        if (target == null)
        {
            return false;
        }
        range = range * range;
        Vector3 currentPos = currentObj.transform.position;
        Vector3 targetPos = target.transform.position;
        return (currentPos - targetPos).sqrMagnitude < range;
    }

    public bool TargetNotAlive(GameObject target)
    {
        if (target != null)
        {
            if (target.GetComponent<Scr_living_stats>() != null)
            {
                return target.GetComponent<Scr_living_stats>().IsDead;
            }
        }
        return true;
    }
}
