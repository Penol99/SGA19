using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Scr_camera_obj_avoid : MonoBehaviour
{

    public LayerMask m_wallsToCollide;


    private CinemachineComposer m_topRig, m_middleRig, m_bottomRig;
    private CinemachineFreeLook m_camFreeLook;
    private CinemachineCollider m_camCollider;
    private Transform m_cam;
    private Transform m_player;
    
 
    private bool m_rightLineToPlayer;
    private bool m_leftLineToPlayer;
    private bool m_pRayR, m_pRayL, m_pRayU, m_pRayD, m_pRayF, m_pRayB;
    private bool m_cRayR, m_cRayL, m_cRayU, m_cRayD, m_cRayF, m_cRayB;
    private bool m_pAnyRays;
    private bool m_cAnyRays;

    //private float orbitCollisionSpeed = 36f;
    private float m_playerAngle;
    private float m_playerRayDist = 2f;
    private float m_cameraRayDist = 2f;
    private float m_orbitSpeed = 6f;

    //NOTE: PLAYER DISTANCE TO CAMERA AND COLLISION

    // Start is called before the first frame update
    void Start()
    {
        m_camFreeLook = GetComponent<CinemachineFreeLook>();
        m_camCollider = GetComponent<CinemachineCollider>();
        CinemachineVirtualCamera tR = m_camFreeLook.GetRig(0);
        CinemachineVirtualCamera mR = m_camFreeLook.GetRig(1);
        CinemachineVirtualCamera bR = m_camFreeLook.GetRig(2);
        m_topRig = tR.GetCinemachineComponent<CinemachineComposer>();
        m_middleRig = mR.GetCinemachineComponent<CinemachineComposer>();
        m_bottomRig = bR.GetCinemachineComponent<CinemachineComposer>();

        
        m_cam = Camera.main.GetComponent<Transform>();
        m_player = m_camFreeLook.m_LookAt;
    }

    void Rays()
    {

        m_rightLineToPlayer = Physics.Linecast(m_cam.position + m_cam.right * 2f, m_player.position, m_wallsToCollide);
        m_leftLineToPlayer = Physics.Linecast(m_cam.position - m_cam.right * 2f, m_player.position, m_wallsToCollide);
       
        m_pRayR = Physics.Raycast(m_player.position, m_player.right, m_playerRayDist, m_wallsToCollide);
        m_pRayL = Physics.Raycast(m_player.position, -m_player.right, m_playerRayDist, m_wallsToCollide);
        m_pRayU = Physics.Raycast(m_player.position, m_player.up, m_playerRayDist, m_wallsToCollide);
        m_pRayD = Physics.Raycast(m_player.position, -m_player.up, m_playerRayDist, m_wallsToCollide);
        m_pRayF = Physics.Raycast(m_player.position, m_player.forward, m_playerRayDist, m_wallsToCollide);
        m_pRayB = Physics.Raycast(m_player.position, -m_player.forward, m_playerRayDist, m_wallsToCollide);

        m_cRayR = Physics.Raycast(m_cam.position, m_cam.right, m_cameraRayDist, m_wallsToCollide);
        m_cRayL = Physics.Raycast(m_cam.position, -m_cam.right, m_cameraRayDist, m_wallsToCollide);
        m_cRayU = Physics.Raycast(m_cam.position, m_cam.up, m_cameraRayDist, m_wallsToCollide);
        m_cRayD = Physics.Raycast(m_cam.position, -m_cam.up, m_cameraRayDist, m_wallsToCollide);
        m_cRayF = Physics.Raycast(m_cam.position, m_cam.forward, m_cameraRayDist, m_wallsToCollide);
        m_cRayB = Physics.Raycast(m_cam.position, -m_cam.forward, m_cameraRayDist, m_wallsToCollide);


        m_pAnyRays = m_pRayR || m_pRayL || m_pRayF || m_pRayB;
        m_cAnyRays = m_cRayR || m_cRayL || m_cRayF || m_cRayB || m_cRayU;

    }

    void OrbitCameraToPlayer()
    {
        // if playerAngle is 1 then its facing forward, if -1 then the player is looking at the camera
        m_playerAngle = Vector3.Dot(m_player.forward, (m_player.position - transform.position).normalized);

        if ((m_pRayR || m_pRayL || m_pRayF || m_pRayB) && (m_playerAngle < 0.8f) && (!m_cRayR && !m_cRayL))
        {
            if (m_rightLineToPlayer && !m_leftLineToPlayer)
                HorizontalOrbit(m_orbitSpeed);

            if (m_leftLineToPlayer && !m_rightLineToPlayer)
                HorizontalOrbit(-m_orbitSpeed);

        }
        
    }

    void HorizontalOrbit(float speed)
    {
        m_camFreeLook.m_XAxis.Value = speed;
    }

    void WallCollision()
    {
        float distToPlayer = Vector3.Distance(m_cam.position, m_player.position);
        float softZone = Mathf.Clamp01((distToPlayer / 8));
        m_topRig.m_SoftZoneHeight = softZone;
        m_topRig.m_SoftZoneWidth = softZone;
        m_middleRig.m_SoftZoneHeight = softZone;
        m_middleRig.m_SoftZoneWidth = softZone;
        m_bottomRig.m_SoftZoneHeight = softZone;
        m_bottomRig.m_SoftZoneWidth = softZone;

    }

    void YAxisRecentering()
    {
        float stickHor = Input.GetAxis("RightHor");
        float stickVer = Input.GetAxis("RightVer");
        if (!Scr_player_controller.FreezePlayer)
            m_camFreeLook.m_YAxisRecentering.m_enabled = (stickVer == 0 && stickHor != 0);

    }

    // Update is called once per frame
    void Update()
    {

        Rays();
        WallCollision();
        YAxisRecentering();

        bool rightStickNull = Input.GetAxis("RightHor") + Input.GetAxis("RightVer") == 0;
        bool leftStickNull = Input.GetAxis("LeftHor") + Input.GetAxis("LeftVer") == 0;


        if (rightStickNull && !leftStickNull)
        {           
            if (!m_cAnyRays)
                OrbitCameraToPlayer();
        }

        // Freeze Camera, only added this code here cause its the only script attached to the freelook
        if (Scr_player_controller.FreezePlayer)
        {
            m_camFreeLook.m_YAxis.m_InputAxisName = "";
            m_camFreeLook.m_XAxis.m_InputAxisName = "";
        } else
        {
            m_camFreeLook.m_YAxis.m_InputAxisName = "RightVer";
            m_camFreeLook.m_XAxis.m_InputAxisName = "RightHor";
        }
        
    }
}
