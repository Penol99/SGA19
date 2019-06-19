using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_interact_shrine : MonoBehaviour, IInteract
{
    public Scr_living_stats m_player;
    public Scr_ui_panel m_interactUI;
    public Vector3 m_spawnPos;


    private float m_enemyRange = 5f;
    private Scr_manlike_find_target m_findTarget;

    private void Start()
    {
        m_findTarget = GetComponent<Scr_manlike_find_target>();
    }

    private void Update()
    {
        bool cancelButton = Input.GetButtonDown("Cancel");
        // This long ass line is so that you can only get out of ui if the first panel on the shrine window is active
        bool cancelUI = cancelButton && Scr_global_canvas.ShrineWindow.GetComponent<Scr_ui_window>().m_panels[0].gameObject.activeInHierarchy;
        if (cancelUI)
        {
            //Scr_player_controller.FreezePlayer = false;
            m_player.GetComponent<Scr_player_controller>().PlayerFreeze(false);
            Scr_global_canvas.ShrineWindow.SetActive(false);
        }
    }

    public void Interact()
    {
        if (!Scr_global_canvas.PauseActive)
        {
            if (m_findTarget.GetTargetInRange(m_enemyRange) == null)
            {
                ChillAtShrine();
            }
            else
            {
                Debug.Log("ENEMY IS CLOSE BY CANT SIT AT SHRINE");
            }
        }
    }

    private void ChillAtShrine()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("ShrineSceneIndex", sceneIndex);
        PlayerPrefs.SetFloat("ShrinePosX", m_spawnPos.x + transform.position.x);
        PlayerPrefs.SetFloat("ShrinePosY", m_spawnPos.y + transform.position.y);
        PlayerPrefs.SetFloat("ShrinePosZ", m_spawnPos.z + transform.position.z);
        ResetEnemyPositions();
        PlayerInteraction();

        // Open UI
        Scr_global_canvas.ShrineWindow.SetActive(true);
        //m_interactUI.OpenPanel(m_interactUI);
    }



    private void PlayerInteraction()
    {
        m_player.StatHealth = m_player.HealthLimit;
        Scr_player_controller.FreezePlayer = true;
    }

    private void ResetEnemyPositions()
    {
        foreach (var enemy in Scr_global_lists.EnemyList)
        {
            if (enemy.GetComponent<Scr_living_stats>() != null)
            {
                enemy.GetComponent<Scr_living_stats>().ResetPosition();
                GoapAction[] enemyAction = enemy.GetComponents<GoapAction>();
                foreach (var action in enemyAction)
                {
                    action.doReset();
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + m_spawnPos, 0.5f);
    }

}
