using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WingType
{
    LEFT,
    RIGHT
}
public class Scr_camera_wings : MonoBehaviour, IOnCheck
{
    public WingType m_wingType;

    public bool m_wingColliding;

    public void OnCheckEnter()
    {
    }
    public void OnCheckExit()
    {
        m_wingColliding = false;
    }
    public void OnCheckStay()
    {
        m_wingColliding = true;
    }
}
