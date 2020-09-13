using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public bool isPlayer;  
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
    private void ShipController_DamageTaken(float value)
    {
        if (value != 0)
        {
            TakeDamage(value);
            return;
        }
        TakeDamage();
    }
    public void TakeDamage(float value)
    {  
        healthPoints -= value;
        checkHealthpoints();
    }
    public void TakeDamage()
    {
        float value = healthPoints;
        SetHealthPoints(value);
        checkHealthpoints();
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
        shipController.OnBeingDestroyed(isPlayer);
        Destroy(gameObject, 0.1f);
    }
}
