using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_living_stats : MonoBehaviour
{
    [SerializeField]
    private float m_statHealth = 100;

    [SerializeField]
    private float m_statStamina = 50;


    private float m_healthLimit;
    private float m_staminaLimit;


    public int SceneIndex { get => SceneManager.GetActiveScene().buildIndex; }
    public float StatHealth { get => m_statHealth; set => m_statHealth = value; }
    public float StatStamina { get => m_statStamina; set => m_statStamina = value; }
    public float HealthLimit { get => m_healthLimit; set => m_healthLimit = value; }
    public float StaminaLimit { get => m_staminaLimit; set => m_staminaLimit = value; }
    public bool IsDead { get => m_statHealth == 0; }
    


    

    

    private void Start()
    {
        HealthLimit = m_statHealth;
        StaminaLimit = m_statStamina;
    }


    public void SubHealth(float amount)
    {
        StatHealth = StatAddSub(StatHealth, -amount, HealthLimit);
    }
    public void SubHealth(float amount, bool stunlock)
    {
        StatHealth = StatAddSub(StatHealth, -amount, HealthLimit);
    }
    public void AddHealth(float amount)
    {
        StatHealth = StatAddSub(StatHealth, amount, HealthLimit);
    }

    public void SubStamina(float amount)
    {
        StatStamina = StatAddSub(StatStamina, -amount, StaminaLimit);
    }
    public void AddStamina(float amount)
    {
        StatStamina = StatAddSub(StatStamina, amount, StaminaLimit);
    }


    float StatAddSub(float value, float amount, float limit)
    {
        value += amount;
        value = Mathf.Clamp(value, 0, limit);
        return value;
    }
}
