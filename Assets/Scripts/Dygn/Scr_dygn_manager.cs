using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DygnPhase { DAY, NIGHT}

public class Scr_dygn_manager : MonoBehaviour
{
    private static DygnPhase CurrentDygnPhase;

    public static void SetDygnPhase(DygnPhase phase)
    {
        CurrentDygnPhase = phase;
    }

    public static DygnPhase GetDygnPhase()
    {
        return CurrentDygnPhase;
    }

}
