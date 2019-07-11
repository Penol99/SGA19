using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CamAxis
{
    YAXIS,
    XAXIS
}
public class Scr_camera_modify_input : MonoBehaviour
{
    private CinemachineFreeLook m_cFreeLook;
    Axis m_xAxis, m_yAxis;


    public struct Axis
    {
        public bool m_isActive;
        public float m_value;
        public float m_inputAxisValue;
        public Axis(bool active,float value, float inputAxisValue)
        {
            m_isActive = active;
            m_value = value;
            m_inputAxisValue = inputAxisValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_xAxis = new Axis(true, 0f, 0f);     
        m_yAxis = new Axis(true, 0f, 0f);     
        m_cFreeLook = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_xAxis.m_isActive)
            m_xAxis.m_inputAxisValue = Input.GetAxis("RightHor");
        else 
            m_cFreeLook.m_XAxis.Value = m_xAxis.m_value;

        if (m_yAxis.m_isActive)
            m_yAxis.m_inputAxisValue = Input.GetAxis("RightVer");
        else  
            m_cFreeLook.m_YAxis.Value = m_yAxis.m_value;

        m_cFreeLook.m_YAxis.m_InputAxisValue = m_yAxis.m_inputAxisValue;
        m_cFreeLook.m_XAxis.m_InputAxisValue = m_xAxis.m_inputAxisValue;
    }

    private void SetAxis(CamAxis axis, bool enable, float value,float inputAxisValue,bool ignoreValue)
    {
        if (axis == CamAxis.XAXIS)
        {
            if (!ignoreValue)
                m_xAxis.m_value = value;
            m_xAxis.m_isActive = enable;
            m_xAxis.m_inputAxisValue = inputAxisValue;
        }
        else
        {
            if (!ignoreValue)
                m_yAxis.m_value = value;
            m_yAxis.m_isActive = enable;
            m_xAxis.m_inputAxisValue = inputAxisValue;
        }
    }

    public void SetAxisActive(CamAxis axis, bool enable)
    {
        SetAxis(axis, enable, 0f, 0f, true);
    }
    public void SetAxisValue(CamAxis axis, float value)
    {
        SetAxis(axis, false, value, 0f, false);
    }
    public void SetAxisInputValue(CamAxis axis, float inputAxisValue)
    {
        SetAxis(axis, false, 0f, inputAxisValue, true);
    }


}
