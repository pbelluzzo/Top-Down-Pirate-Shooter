using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public delegate void OnDamageTakeHandler(float value);
    public event OnDamageTakeHandler DamageTaken;
    [SerializeField] private float healthPoints = 100;

    public void takeDamage(float value)
    {

        healthPoints -= value;
        checkDestruction();
        DamageTaken?.Invoke(healthPoints);
    }
    private void checkDestruction()
    {
        if(healthPoints <= 0)
        {
            Destroy();
        }
    }
    public void Destroy()
    {

    }
}
