using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Scr_manlike_input : MonoBehaviour
{
    public float m_walkSpeed = 3f;
    public float m_runSpeed = 4.3f;

    public float m_staminaRollLoss = 15f;
    public float m_staminaRunGain = 24f;
    public float m_staminaRunLoss = 8f;
    public float m_runDelay = .7f;

    private float m_runDelayTimer = 0f;
    private float m_currentMoveSpeed = 0f;
    private float m_moveVelocity;
    private bool m_runTrigger;
    private bool m_r1Trigger;
    private bool m_r2Trigger;
    private bool m_l1Trigger;
    private bool m_l2Trigger;
    private bool m_stunlockTrigger;
    private bool m_rollTrigger; // In player controller, this should be set to a button that releases

    public float MoveVelocity { get => m_moveVelocity; set => m_moveVelocity = value; }
    public bool R1Trigger { get => m_r1Trigger; set => m_r1Trigger = value; }
    public bool R2Trigger { get => m_r2Trigger; set => m_r2Trigger = value; }
    public bool L1Trigger { get => m_l1Trigger; set => m_l1Trigger = value; }
    public bool L2Trigger { get => m_l2Trigger; set => m_l2Trigger = value; }
    public bool RollTrigger { get => m_rollTrigger && RunDelayTimer < m_runDelay; set => m_rollTrigger = value; }
    public float CurrentMoveSpeed { get => m_currentMoveSpeed; set => m_currentMoveSpeed = value; }
    public bool RunTrigger { get => m_runTrigger; set => m_runTrigger = value; }
    public float RunDelayTimer { get => m_runDelayTimer; set => m_runDelayTimer = value; }
    public bool StunlockTrigger { get => m_stunlockTrigger; set => m_stunlockTrigger = value; }

    private void LateUpdate()
    {
        MoveVelocity = Mathf.Clamp(MoveVelocity, 0, CurrentMoveSpeed);
        
    }



}
