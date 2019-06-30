using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_living_standard_hurt : MonoBehaviour, IHurt
{
    public void Hurt(float damage, GameObject damager)
    {
        Debug.Log("DANGER CUBE IS HURT");
        GetComponent<Scr_living_stats>().SubHealth(damage);
    }

}
