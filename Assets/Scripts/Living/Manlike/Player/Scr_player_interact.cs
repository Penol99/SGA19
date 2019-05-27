using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_player_interact : MonoBehaviour
{

    private float m_interactRange = 3f;
    private float m_interactAngle = 0.01f;
    private Scr_save_load_game saveManager;

    private void Awake()
    {
        saveManager = GameObject.FindObjectOfType<Scr_save_load_game>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interaction();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
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
            {
                saveManager.SaveGame();
                obj.GetComponent<IInteract>().Interact();
            }
        }
    }
}
