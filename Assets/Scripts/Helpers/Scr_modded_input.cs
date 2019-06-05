using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_modded_input : MonoBehaviour
{
    public static bool DPadLeft, DPadRight;

    private bool m_dpadPressedRight, m_dpadPressedLeft;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetDPadPress();
    }

    private void SetDPadPress()
    {
        DPadRight = false;
        DPadLeft = false;
        float horAxis = Input.GetAxis("DPadHor");
        if (!m_dpadPressedRight && horAxis > 0)
        {
            m_dpadPressedLeft = false;
            m_dpadPressedRight = true;
            DPadRight = true;
        }
        if (!m_dpadPressedLeft && horAxis < 0)
        {
            m_dpadPressedLeft = true;
            m_dpadPressedRight = false;
            DPadLeft = true;
        }
        if (horAxis <= 0)
        {
            m_dpadPressedRight = false;
        }
        if (horAxis >= 0)
        {
            m_dpadPressedLeft = false;
        }
    }




}
