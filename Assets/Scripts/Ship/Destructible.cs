using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public bool isPlayer;

    public delegate void OnDamageTakeHandler(float value);
    public delegate void OnDestroyHandler(Destructible destructible, bool isPlayer);

    public event OnDamageTakeHandler DamageTaken;
    public event OnDestroyHandler OnBeingDestroyed;
    
    [SerializeField] private float healthPoints = 100;

    [Header("Damaged Thresholds")]
    [SerializeField] private float healthThreshold0;
    [SerializeField] private float healthThreshold1;
    [SerializeField] private float healthThreshold2;

    private int currentThreshold = 0;

    private ShipController shipController;

    private void Awake()
    {
        shipController = GetComponent<ShipController>();
        shipController.DamageTaken += ShipController_DamageTaken;

    }
    private void SetHealthPoints(float value)
    {
        if ((healthPoints -= value) <= 0)
        {
            healthPoints = 0;
            return;
        }
        healthPoints -= value;
    }
    private void ShipController_DamageTaken()
    {
        TakeDamage();
    }
    public void TakeDamage(float value)
    {  
        healthPoints -= value;
        checkHealthpoints();
        DamageTaken?.Invoke(healthPoints);
    }
    public void TakeDamage()
    {
        float value = healthPoints;
        SetHealthPoints(value);
        checkHealthpoints();
        DamageTaken?.Invoke(healthPoints);
    }
    private void checkHealthpoints()
    {
        if (healthPoints <= 0)
        {
            Destroy();
            return;
        }
        CheckHealthThresholds();
    }

    private void CheckHealthThresholds()
    {
        if (healthPoints > healthThreshold1 && currentThreshold != 0)
        {
            currentThreshold = 0;
            shipController.OnHealthThresholdChanged(currentThreshold);
        }
        if (healthPoints <= healthThreshold1 && healthPoints > healthThreshold2 && currentThreshold != 1)
        {           
            currentThreshold = 1;
            shipController.OnHealthThresholdChanged(currentThreshold);
        }
        if (healthPoints <= healthThreshold2 && currentThreshold != 2)
        {
            currentThreshold = 2;
            shipController.OnHealthThresholdChanged(currentThreshold);      
        }
    }

    public void Destroy()
    {
        OnBeingDestroyed?.Invoke(this, isPlayer);
        Destroy(this.gameObject, 0.1f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Debug.Log("collision cannon ball");
            float damageValue = collision.gameObject.GetComponent<CannonBall>().GetDamage();
            TakeDamage(damageValue);
        }
    }
}
