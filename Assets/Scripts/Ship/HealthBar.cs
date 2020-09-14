using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject fill;
    private ShipController shipController;

    private void Awake()
    {
        shipController = GetComponentInParent<ShipController>();
        shipController.DamageTaken += ShipController_OnDamageTaken;
    }

    private void ShipController_OnDamageTaken(float value)
    {
        UpdateHealthBar(value);
    }

    public void UpdateHealthBar(float value)
    {
        Vector3 newTransform = new Vector3((fill.transform.localScale.x - value /100), fill.transform.localScale.y);
        if (newTransform.x < 0) newTransform.x = 0;
        fill.transform.localScale = newTransform;
    }

}
