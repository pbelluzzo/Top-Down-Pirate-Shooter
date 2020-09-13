﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public bool isPlayer;

    public delegate void OnDamageTakeHandler(float value);

    public event OnDamageTakeHandler DamageTaken;
    [SerializeField] private float healthPoints = 100;

    public void TakeDamage(float value)
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
