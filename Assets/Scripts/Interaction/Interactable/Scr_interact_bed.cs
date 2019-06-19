using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_bed : MonoBehaviour, IInteract
{

    public static bool CanGoToBed;
    private void Start()
    {
        CanGoToBed = true;
    }

    public void Interact()
    {
        if (CanGoToBed)
        {   
            Scr_global_canvas.SetPanelActive(Scr_global_canvas.BedPanel, true, true);
            Scr_player_controller.FreezePlayer = true;
        }
    }


}
