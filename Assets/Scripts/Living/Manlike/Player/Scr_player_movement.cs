using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Scr_player_controller))]
public class Scr_player_movement : MonoBehaviour
{
    /// <summary>
    /// The purpose of this class is to handle all the states code when it comes to controling the player inside
    /// the world, transform changes to the player happens here.
    /// This class handles for now, all state switching, it communicates with the Scr_player_controller class
    /// which contains the actual statemachine, where the actual state switching happens.
    /// </summary>

    private Scr_living_fall_damage m_fallDmg;
    private CharacterController m_cc;
    private Scr_player_controller m_pCon;
    private Transform m_cam;
    private Vector3 m_moveDir = Vector3.zero;
    private float m_rotateSpeed = 18f;
    private float m_gravity = 20f;

    void Start()
    {
        m_fallDmg = GetComponent<Scr_living_fall_damage>();
        m_cam = Camera.main.GetComponent<Transform>();
        m_pCon = GetComponent<Scr_player_controller>();
        m_cc = GetComponent<CharacterController>();
    }


    public void GotoPosition(Vector3 pos)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = pos;
        GetComponent<CharacterController>().enabled = true;
    }

    //--ROTATE-PLAYER-WITH-MOVEMENT-STICK
    public void LeftStickRotation()
    {
        float leftStickValue = (Mathf.Abs(m_pCon.LHor) + Mathf.Abs(m_pCon.LVer));
        if (leftStickValue > 0)
        {
            // Left stick rotation
            float stickAngle = Mathf.Atan2(m_pCon.LHor, -m_pCon.LVer) * Mathf.Rad2Deg;
            Quaternion stickRot = Quaternion.Euler(0f, stickAngle, 0f);
            // Camera rotation
            Quaternion camRot = new Quaternion(transform.rotation.x, m_cam.rotation.y, transform.rotation.z, m_cam.rotation.w);
            // Add both the camera and joystick quaternions to the player
            transform.rotation = Quaternion.Lerp(transform.rotation, camRot * stickRot, Time.deltaTime * m_rotateSpeed);
        }
    }

    //--GET-VELOCITY-FROM-MOVEMENT-STICK-AXIS-VALUES
    public float SetMoveVelocity(float moveSpeed, float moveVelocity)
    {
        // Add the 2 Dimensions of the joystick into an absolute 1 Dimensional float multiplied by speed
        float x = Mathf.Abs(m_pCon.LHor);
        float y = Mathf.Abs(m_pCon.LVer);


        if (!Scr_player_controller.FreezePlayer)
        {
            LeftStickRotation();
            moveVelocity = Scr_math_formulas.SquareToCircle(x, y) * moveSpeed;
        }
        else
            moveVelocity = 0;


        Gravity();        
        return moveVelocity;
    }

    public void Gravity()
    {
        if (!m_fallDmg.OnGround)
            m_moveDir.y -= m_gravity * Time.deltaTime;
        else
            m_moveDir.y = 0;//-m_gravity * Time.deltaTime; 

        m_cc.Move(m_moveDir * Time.deltaTime);
    }
}
