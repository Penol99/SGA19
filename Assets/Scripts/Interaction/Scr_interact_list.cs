using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_list : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Scr_global_lists.InteractList.Add(GetComponent<Transform>());
    }

}
