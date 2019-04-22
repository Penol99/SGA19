using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_door : MonoBehaviour, IInteract
{

    public int m_angleOpen;

    private bool m_doorOpen = false;
    private float m_doorAngle;
    private float m_doorSpeed = 300f;

    public void Interact()
    {       
        m_doorOpen = !m_doorOpen;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Interact();

        if (m_doorOpen)
        {
            if (m_doorAngle > m_angleOpen-90)
                m_doorAngle -= Time.deltaTime * m_doorSpeed;
        }
        else
        {
            if (m_doorAngle <= m_angleOpen)
                m_doorAngle += Time.deltaTime * m_doorSpeed;
        }

        transform.eulerAngles = new Vector3(0f, m_doorAngle, 0f);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .25f);
        
    }
}
