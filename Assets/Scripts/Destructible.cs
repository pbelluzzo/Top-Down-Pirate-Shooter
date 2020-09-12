using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float healthPoints = 100;

    public void takeDamage(float value)
    {
        healthPoints -= value;
        checkDestruction();
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
