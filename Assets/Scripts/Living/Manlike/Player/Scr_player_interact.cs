using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_player_interact : MonoBehaviour
{
    private bool m_canInteract;

    private float m_interactRange = 3f;
    private float m_interactAngle = 0.01f;
    private Scr_save_load_game saveManager;

    private void Awake()
    {
        m_canInteract = true;
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
        m_canInteract = !Scr_global_canvas.PauseMenuPanel.activeInHierarchy ||
                      !Scr_global_canvas.ShrineWindow.activeInHierarchy ||
                      !Scr_global_canvas.DialogueBox.activeInHierarchy;

        if (m_canInteract && !Scr_player_controller.FreezePlayer)
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
}
