using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_dygn_enviroment : MonoBehaviour
{

    public GameObject m_dayTimeObjects, m_nightTimeObjects;
    private Scr_dygn_manager m_dygnManager;

    // Start is called before the first frame update
    void Start()
    {
        m_dygnManager = FindObjectOfType<Scr_dygn_manager>();

        if (m_dygnManager.GetDygnPhase().Equals(DygnPhase.DAY))
        {
            m_dayTimeObjects.SetActive(true);
            m_nightTimeObjects.SetActive(false);
        } else
        {
            m_dayTimeObjects.SetActive(false);
            m_nightTimeObjects.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
