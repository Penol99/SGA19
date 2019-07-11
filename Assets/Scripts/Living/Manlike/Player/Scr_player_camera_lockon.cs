using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Scr_player_camera_lockon : MonoBehaviour
{
    private float m_lockOnRange = 13f;
    private float m_midPosLerpSpeed = 4f;
    private float m_midPosLerpCount;
    private GameObject m_target;
    private GameObject m_midPoint;
    private CinemachineFreeLook m_camFreeLook;
    private Scr_camera_modify_input m_camModifyInput;  
    private Scr_manlike_input m_manInput;
    private Scr_manlike_find_target m_findTarget;
    private Scr_camera_wings m_leftWing;
    private Scr_camera_wings m_rightWing;
    private bool m_lockedOn;
    private string m_yAxisName;
    private LayerMask m_inTheWayLayer;


    // Start is called before the first frame update
    void Start()
    {
        m_inTheWayLayer = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Default");
        foreach (var item in FindObjectsOfType<Scr_camera_wings>())
        {
            if (item.m_wingType.Equals(WingType.LEFT))
                m_leftWing = item;
            else if (item.m_wingType.Equals(WingType.RIGHT))
                m_rightWing = item;
        }


        m_midPoint = new GameObject();
        m_midPoint.name = "lock_on_midPoint";
        m_midPoint.SetActive(false);
        m_camFreeLook = FindObjectOfType<Scr_camera_obj_avoid>().GetComponent<CinemachineFreeLook>();
        m_camModifyInput = m_camFreeLook.GetComponent<Scr_camera_modify_input>();
        m_findTarget = GetComponent<Scr_manlike_find_target>();
        m_manInput = GetComponent<Scr_manlike_input>();        
    }

    // Update is called once per frame
    void Update()
    {
        bool objectInTheWay;
        if (m_target != null)
            objectInTheWay = Physics.Linecast(transform.position, m_target.transform.position, m_inTheWayLayer);
        else
            objectInTheWay = true;

        if (m_manInput.L1Trigger)
        {
            GameObject target = m_findTarget.GetTargetInRange(m_lockOnRange);
            if (target != null && !m_lockedOn && !objectInTheWay)
                LockOn(target);
            else if (m_lockedOn)
                StopLockOn();
        }

        if (m_lockedOn)
        {
            Vector3 midPos = (transform.position + m_target.transform.position) / 2;
            m_midPosLerpCount += m_midPosLerpSpeed * Time.deltaTime;
            m_midPoint.transform.position = Vector3.Lerp(transform.position,midPos,m_midPosLerpCount);

            if (m_leftWing.m_wingColliding)
                m_camModifyInput.SetAxisInputValue(CamAxis.XAXIS, .5f);
            else if (m_rightWing.m_wingColliding)
                m_camModifyInput.SetAxisInputValue(CamAxis.XAXIS, -.5f);
            else if (!m_rightWing.m_wingColliding && !m_rightWing.m_wingColliding)
                m_camModifyInput.SetAxisInputValue(CamAxis.XAXIS, 0f);

            
            
            // When out of range
            if (Vector3.Distance(transform.position,m_target.transform.position) >= m_lockOnRange || objectInTheWay)
            {
                StopLockOn();
            }
        } else
        {
            if (m_midPoint.activeInHierarchy)
            {
                Vector3 playerPos = transform.position;
                Vector3 midPos = m_midPoint.transform.position;
                bool midOnPlayer = (Mathf.Round(playerPos.x) == Mathf.Round(midPos.x) &&
                                    Mathf.Round(playerPos.y) == Mathf.Round(midPos.y) &&
                                    Mathf.Round(playerPos.z) == Mathf.Round(midPos.z));
                if (!midOnPlayer)
                {
                    Debug.Log(Vector3.Dot(m_midPoint.transform.position, transform.position));
                    m_midPosLerpCount += m_midPosLerpSpeed/2 * Time.deltaTime;
                    m_midPoint.transform.position = Vector3.Lerp(m_midPoint.transform.position, transform.position, m_midPosLerpCount);
                }
                else
                {
                    m_midPosLerpCount = 0f;
                    m_midPoint.SetActive(false);
                    m_camFreeLook.m_LookAt = transform;
                    m_camModifyInput.SetAxisActive(CamAxis.YAXIS, true);
                    m_camModifyInput.SetAxisActive(CamAxis.XAXIS, true);
                    m_target = null;
                }
            }
        }
    }

    private void LockOn(GameObject target)
    {
        m_camModifyInput.SetAxisValue(CamAxis.YAXIS, 0.5f);
        m_camModifyInput.SetAxisActive(CamAxis.XAXIS, false);
        m_target = target;
        m_lockedOn = true;
        m_midPoint.SetActive(true);
        m_camFreeLook.m_LookAt = m_midPoint.transform;

    }

    private void StopLockOn()
    {
        m_midPosLerpCount = 0f;       
        m_lockedOn = false;     
               
    }
}
