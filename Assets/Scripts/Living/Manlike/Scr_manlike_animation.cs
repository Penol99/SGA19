using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scr_manlike_input))]
public class Scr_manlike_animation : MonoBehaviour
{

    private Scr_living_stats m_stats;
    private Animator m_animator;
    private Scr_manlike_input m_manInput;

    private float m_moveVelBeforePause;

    private bool m_rollStart, m_rollEnd;
    private bool m_lightAttackStart, m_lightAttackEnd;
    private bool m_idleWalkRun;
    private bool m_shield;

    public bool RollStart { get => m_rollStart; set => m_rollStart = value; }
    public bool RollEnd { get => m_rollEnd; set => m_rollEnd = value; }
    public bool LightAttackStart { get => m_lightAttackStart; set => m_lightAttackStart = value; }
    public bool LightAttackEnd { get => m_lightAttackEnd; set => m_lightAttackEnd = value; }
    public bool IdleWalkRun { get => m_idleWalkRun; set => m_idleWalkRun = value; }
    public bool Shield { get => m_shield; set => m_shield = value; }




    // Start is called before the first frame update
    void Start()
    {
        m_stats = GetComponent<Scr_living_stats>();
        m_manInput = GetComponent<Scr_manlike_input>();
        m_animator = GetComponent<Animator>();
    }

    public void RollStaminaDepletion()
    {
        m_stats.SubStamina(m_manInput.m_staminaRollLoss);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetVariables();
        AnimatorStateInfo();
    }

    void SetVariables()
    {


        m_animator.SetFloat("velocity_walk_run", m_manInput.MoveVelocity / m_manInput.m_runSpeed);
        
        m_animator.SetFloat("stamina", m_stats.StatStamina);

        m_animator.SetBool("shield_held", m_manInput.L1Trigger && !m_rollStart && !m_rollEnd);


        if (m_manInput.R1Trigger && !m_lightAttackStart)
        {
            m_animator.SetTrigger("trigger_light_attack");
            m_manInput.R1Trigger = false;
            
        }

        if (m_manInput.RollTrigger && !m_rollStart && m_stats.StatStamina > 0 && m_manInput.MoveVelocity > 0)
        {                        
            m_animator.SetTrigger("trigger_roll");
            m_manInput.RollTrigger = false;
        }

        if (m_manInput.StunlockTrigger)
        {
            m_animator.SetTrigger("trigger_stunlock");
            m_manInput.StunlockTrigger = false;
        }
       
    }
    
    public void ResetLightAttack()
    {
        m_animator.ResetTrigger("trigger_light_attack");
    }

    void AnimatorStateInfo()
    {

        m_idleWalkRun = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Ground Actions.Walking/Running");
        m_rollStart = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Ground Actions.Armature|Roll Start") ||
                      m_animator.GetCurrentAnimatorStateInfo(0).IsName("Ground Actions.Armature|Roll Start 0");
        m_rollEnd = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Ground Actions.Armature|Roll End") ||
                    m_animator.GetCurrentAnimatorStateInfo(0).IsName("Ground Actions.Armature|Roll End 0");
        m_lightAttackStart = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Combat.Armature|Slash 1 Start") ||
                             m_animator.GetCurrentAnimatorStateInfo(0).IsName("Combat.Armature|Slash 2 Start");
        m_lightAttackEnd = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Combat.Armature|Slash 1 End") ||
                           m_animator.GetCurrentAnimatorStateInfo(0).IsName("Combat.Armature|Slash 2 End");
        m_shield = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Ground Actions.Armature|Shield") ||
                   m_animator.GetCurrentAnimatorStateInfo(1).IsName("Ground Actions.Armature|Shield");// If theres more shield animations then add that to this bool.

    }
}
