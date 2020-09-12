using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject fill;
    private Destructible destructibleObject;

    private void Awake()
    {
        destructibleObject = GetComponentInParent<Destructible>();
        destructibleObject.DamageTaken += DestructibleObject_OnDamageTaken;
    }

    private void DestructibleObject_OnDamageTaken(float value)
    {
        UpdateHealthBar(value);
    }

    public void UpdateHealthBar(float value)
    {
        Vector3 newTransform = new Vector3(value /100, fill.transform.localScale.y);
        fill.transform.localScale = newTransform;
    }

}
