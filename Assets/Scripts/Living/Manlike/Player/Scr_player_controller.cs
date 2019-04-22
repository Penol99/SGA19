﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scr_manlike_input))]
[RequireComponent(typeof(Scr_player_movement))]
public class Scr_player_controller : MonoBehaviour, IPlayerStates
{
    public PBaseState m_baseState;
    public PSubState m_subState;

    
    private CharacterController m_cc;
    private Scr_manlike_animation m_manAnim;
    private Scr_manlike_input m_manInput;
    private Scr_player_movement m_pMov;
    private Scr_living_stats m_stats;
    private float m_rVer, m_rHor, m_lVer, m_lHor;
    private bool m_r1ActionBtnDown;
    private bool m_l1ActionBtn;

    
    public bool m_canRoll;
    public bool m_canRun;


    public float LHor { get => m_lHor; set => m_lHor = value; }
    public float LVer { get => m_lVer; set => m_lVer = value; }
    public float RHor { get => m_rHor; set => m_rHor = value; }
    public float RVer { get => m_rVer; set => m_rVer = value; }
    public bool R1ActionBtnDown { get => m_r1ActionBtnDown; set => m_r1ActionBtnDown = value; }
    public bool L1ActionBtn { get => m_l1ActionBtn; set => m_l1ActionBtn = value; }



    // Start is called before the first frame update
    void Start()
    {
        m_stats = GetComponent<Scr_living_stats>();
        m_manAnim = GetComponent<Scr_manlike_animation>();
        m_manInput = GetComponent<Scr_manlike_input>();
        m_pMov = GetComponent<Scr_player_movement>();
        m_cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        ControllerInput();
    }

    void ControllerInput()
    {
        LHor = Input.GetAxis("LeftHor");
        LVer = Input.GetAxis("LeftVer");
        RHor = Input.GetAxis("RightHor");
        RVer = Input.GetAxis("RightVer");
        m_manInput.RunTrigger = Input.GetButton("Run");
        m_manInput.RollTrigger = Input.GetButtonUp("Roll");
        m_manInput.R1Trigger = Input.GetButtonDown("R1Action");
        m_manInput.L1Trigger = Input.GetButton("L1Action");
        
    }
    void StateMachine()
    {
        switch (m_baseState)
        {
            case PBaseState.MOVE:
                BS_Move();
                break;
            case PBaseState.ROLL:
                BS_Roll();
                break;
            case PBaseState.ATTACK1:
                BS_Attack1();
                break;
            default:
                break;
        }
        switch (m_subState)
        {
            case PSubState.SHIELD:
                SS_Shield();
                break;
            default:
                break;
        }
    }

    public bool IsOnState(PBaseState baseState)
    {
        return baseState.Equals(m_baseState);
    }
    public bool IsOnState(PSubState subState)
    {
        return subState.Equals(m_subState);
    }
    public void ChangeState(PBaseState baseState)
    {
        m_baseState = baseState;
    }
    public void ChangeState(PBaseState baseState, bool condition)
    {
        if (condition)
            m_baseState = baseState;
    }
    public void ChangeState(PSubState subState)
    {
        m_subState = subState;
    }
    public void ChangeState(PSubState subState, bool condition)
    {
        if (condition)
            m_subState = subState;
    }

    public void BS_Move()
    {
        bool inRun = m_manInput.RunTrigger && m_stats.StatStamina > 0 && m_manInput.RunDelayTimer >= m_manInput.m_runDelay;
        int intInRun = System.Convert.ToInt16(inRun);
        int intOffRun = System.Convert.ToInt16(!inRun);
        // Delay Timer resets to 0 when runbtn is not held
        m_manInput.RunDelayTimer = (m_manInput.RunDelayTimer + Time.deltaTime) * System.Convert.ToInt16(m_manInput.RunTrigger);
        m_manInput.CurrentMoveSpeed = (m_manInput.m_walkSpeed * intOffRun) + (m_manInput.m_runSpeed * intInRun);
        m_manInput.MoveVelocity = m_pMov.Move(m_manInput.CurrentMoveSpeed, m_manInput.MoveVelocity);

        m_stats.AddStamina(m_manInput.m_staminaRunGain * Time.deltaTime * intOffRun);
        m_stats.SubStamina(m_manInput.m_staminaRunLoss * Time.deltaTime * intInRun);

        ChangeState(PBaseState.ATTACK1, m_manAnim.LightAttackStart);
        ChangeState(PBaseState.ROLL, m_manAnim.RollStart);

    }

    public void BS_Roll()
    {
        m_pMov.Gravity();
        if (!m_manAnim.RollEnd)
            m_pMov.LeftStickRotation();

        ChangeState(PBaseState.MOVE, !m_manAnim.RollStart && m_manAnim.IdleWalkRun);
    }

    public void BS_Attack1()
    {
        m_pMov.Gravity();
        if (!m_manAnim.LightAttackEnd)
            m_pMov.LeftStickRotation();

        ChangeState(PBaseState.MOVE, !m_manAnim.LightAttackStart && m_manAnim.IdleWalkRun);
    }

    public void SS_Shield()
    {

    }

    
}
