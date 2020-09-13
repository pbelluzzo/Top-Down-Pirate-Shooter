using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVisualsManager : MonoBehaviour
{
    [SerializeField] private ShipSprites shipSprites;
    [SerializeField] private SpriteRenderer shipHull;
    [SerializeField] private SpriteRenderer shipSail;

    [SerializeField] private GameObject destructionEffect;
    [SerializeField] private GameObject damagedEffect;

    [SerializeField] private ShipController shipController;

    private void Awake()
    {
        shipController.HealthThresholdChanged += ShipController_HealthThresholdChanged;
    }

    private void ShipController_HealthThresholdChanged(int threshold)
    {
        SetHullSprite(threshold);
        SetSailSprite(threshold);
        if (threshold == 2) RenderDamagedEffect();
    }

    private void SetHullSprite(int i)
    {
        shipHull.sprite = shipSprites.hulls[i];
    }
    private void SetSailSprite(int i)
    {
        shipSail.sprite = shipSprites.sails[i];
    }
    private void RenderDamagedEffect()
    {
        Instantiate(damagedEffect, gameObject.transform);
    }
}
