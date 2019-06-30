using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerItem
{
    HEALTH,
    MONEY
}
public class Scr_player_equipment : MonoBehaviour
{
    public void AddItem(PlayerItem item, int amount)
    {
        switch (item)
        {
            case PlayerItem.HEALTH:
                    GetComponent<Scr_living_stats>().AddHealth(amount);
                break;
            case PlayerItem.MONEY:
                    GetComponent<Scr_player_rpg_stats>().m_money += amount;
                break;
            default:
                break;
        }
    }
}
