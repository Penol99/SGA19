using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_player_interact : MonoBehaviour
{

    private float m_interactRange = 3f;
    private float m_interactAngle = 0.01f;


    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interaction();
        }
    }


    void Interaction()
    {
        for (int i = 0; i < Scr_global_lists.InteractList.Count; i++)
        {
            
            Transform obj = Scr_global_lists.InteractList[i];           
            Vector3 thisPos = transform.position;
            Vector3 objPos = obj.position;
            float facingAngle = Vector3.Dot(transform.forward, (objPos - thisPos).normalized);
            float distToObj = (thisPos - objPos).sqrMagnitude;

            if ((distToObj < m_interactRange) && (facingAngle >= m_interactAngle))
                obj.GetComponent<IInteract>().Interact();
        }
    }
}
