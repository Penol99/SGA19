using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_modded_input : MonoBehaviour
{
    public static bool LStickLeft, LStickRight;

    private bool m_lStickPressedRight, m_lStickPressedLeft;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetLeftStickPress();
    }

    private void SetLeftStickPress()
    {
        LStickRight = false;
        LStickLeft = false;
        float horAxis = Input.GetAxis("LeftHor");
        if (!m_lStickPressedRight && horAxis > 0)
        {
            m_lStickPressedLeft = false;
            m_lStickPressedRight = true;
            LStickRight = true;
        }
        if (!m_lStickPressedLeft && horAxis < 0)
        {
            m_lStickPressedLeft = true;
            m_lStickPressedRight = false;
            LStickLeft = true;
        }
        if (horAxis <= 0)
        {
            m_lStickPressedRight = false;
        }
        if (horAxis >= 0)
        {
            m_lStickPressedLeft = false;
        }
    }




}
