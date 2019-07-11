using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_ladder : MonoBehaviour, IInteract
{
    Scr_living_fall_damage m_player;


    private void Start()
    {
        m_player = FindObjectOfType<Scr_player_controller>().GetComponent<Scr_living_fall_damage>();
    }

    public void Interact()
    {
        if (m_player.OnGround)
        {

        }
    }
}
