﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGunpowderBarrels : MonoBehaviour
{
    private ShipController shipController;

    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionDamage;

    private void Awake()
    {
        shipController = GetComponent<ShipController>();
        shipController.BeingDestroyed += ShipController_OnBeingDestroyed;
    }

    private void ShipController_OnBeingDestroyed(ShipController controller, bool isPlayer)
    {
        shipController.BeingDestroyed -= ShipController_OnBeingDestroyed;
        DamageNearbyShips();
    }

    private void DamageNearbyShips()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach(Collider2D enemy in enemies)
        {
            if (enemy.gameObject.GetComponent<ShipController>() != null)
            {
                enemy.gameObject.GetComponent<ShipController>().OnDamageTaken(explosionDamage);
            }
        }
    }
}
