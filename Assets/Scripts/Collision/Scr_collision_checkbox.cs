using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_collision_checkbox : MonoBehaviour
{
    public bool m_displayGizmo;
    public LayerMask m_checkLayer;
    public Vector3 m_positionOffset = Vector3.zero;
    public Vector3 m_boxSize = Vector3.one;

    private bool m_checkEnter;
    private bool m_checkStay;

    // Update is called once per frame
    void Update()
    {
        bool onCheck = Physics.CheckBox(transform.position + m_positionOffset, Vector3.Scale(transform.localScale,m_boxSize / 2), transform.rotation, m_checkLayer);
        if (onCheck)
        {
            m_checkStay = true;
            foreach (var item in GetComponents<IOnCheck>())
            {
                if (item != null)
                {
                    item.OnCheckStay();
                    if (m_checkStay && !m_checkEnter)
                    {
                        m_checkEnter = true;
                        item.OnCheckEnter();
                    }
                }
            }
        } else
        {
            if (m_checkStay)
            {
                foreach (var item in GetComponents<IOnCheck>())
                {
                    if (item != null)
                    {
                        item.OnCheckExit();
                    }
                }
            }
            m_checkStay = false;
            m_checkEnter = false;
        }
        
    }

    private void OnDrawGizmos()
    {
        if (m_displayGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(m_positionOffset,m_boxSize);
        }
    }
}
