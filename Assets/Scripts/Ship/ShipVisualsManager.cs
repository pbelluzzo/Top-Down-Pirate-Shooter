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
        shipController.BeingDestroyed += ShipController_BeingDestroyed;
    }

    private void ShipController_BeingDestroyed(ShipController controller, bool isPlayer)
    {
        GameObject explosionFx = Instantiate(destructionEffect, transform.position, transform.rotation);
        Destroy(explosionFx, 1f);
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
