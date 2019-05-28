using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_interact_shrine : MonoBehaviour, IInteract
{
    public Scr_living_stats m_player;
    public Vector3 m_spawnPos;


    private bool m_shrineActive;
    private float m_enemyRange = 5f;
    private Scr_manlike_find_target m_findTarget;

    private void Start()
    {
        m_findTarget = GetComponent<Scr_manlike_find_target>();
    }

    private void Update()
    {
        bool cancelButton = Input.GetButtonDown("Cancel");
        if (m_shrineActive && cancelButton)
        {        
            Scr_player_controller.FreezePlayer = false;
            m_shrineActive = false;
        }
    }

    public void Interact()
    {
        if (m_findTarget.GetTargetInRange(m_enemyRange) == null)
        {
            ChillAtShrine();
        } else
        {
            Debug.Log("ENEMY IS CLOSE BY CANT SIT AT SHRINE");
        }
    }

    private void ChillAtShrine()
    {
        m_shrineActive = true;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("ShrineSceneIndex", sceneIndex);
        PlayerPrefs.SetFloat("ShrinePosX", m_spawnPos.x + transform.position.x);
        PlayerPrefs.SetFloat("ShrinePosY", m_spawnPos.y + transform.position.y);
        PlayerPrefs.SetFloat("ShrinePosZ", m_spawnPos.z + transform.position.z);
        ResetEnemyPositions();
        PlayerInteraction();
    }

    private void PlayerInteraction()
    {
        m_player.StatHealth = m_player.HealthLimit;
        Scr_player_controller.FreezePlayer = true;
        if (Input.GetButtonDown("Cancel"))
        {
            Scr_player_controller.FreezePlayer = false;
        }
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
