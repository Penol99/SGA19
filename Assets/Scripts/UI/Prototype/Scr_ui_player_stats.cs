using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_ui_player_stats : MonoBehaviour
{

    public Scr_living_stats playerStats;
    public Slider healthBar,staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.value = playerStats.StatStamina / playerStats.StaminaLimit;
        healthBar.value = playerStats.StatHealth / playerStats.HealthLimit;

    }
}
