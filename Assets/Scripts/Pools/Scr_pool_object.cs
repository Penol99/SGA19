using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_pool_object : MonoBehaviour, IOnCheck
{
    // This variable can go public if i where to need that.
    private int m_itemAddAmount = 1;

    public PlayerItem m_item;
    public bool m_destroyOnCollision;
    public Scr_pool m_pool;
    public Scr_player_controller m_player;
    public bool m_hoverToPlayer;
    private float m_hoverSpeed = 5f;


    public void Despawn()
    {
        transform.parent = m_pool.transform;
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (m_hoverToPlayer)
        {
            HoverToPlayer();
        }
    }

    private void HoverToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position+Vector3.up, m_hoverSpeed * Time.deltaTime);
    }

    public void OnCheckEnter()
    {
        if (m_destroyOnCollision)
        {
            m_player.GetComponent<Scr_player_equipment>().AddItem(m_item,m_itemAddAmount);
            Despawn();
        }
    }

    public void OnCheckStay()
    {
    }

    public void OnCheckExit()
    {
    }
}
