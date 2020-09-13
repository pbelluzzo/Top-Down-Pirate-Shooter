using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGunpowderBarrels : MonoBehaviour
{
    private Destructible destructible;

    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionDamage;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();
        destructible.OnBeingDestroyed += Destructible_OnBeingDestroyed;
    }

    private void Destructible_OnBeingDestroyed(Destructible destructible, bool isPlayer)
    {
        destructible.OnBeingDestroyed -= Destructible_OnBeingDestroyed;
        DamageNearbyShips();
    }

    private void DamageNearbyShips()
    {
        Collider[] ships = Physics.OverlapSphere(transform.position, explosionRange);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach(Collider2D enemy in enemies)
        {
            if (enemy.gameObject.GetComponent<Destructible>() != null)
            {
                enemy.gameObject.GetComponent<Destructible>().TakeDamage(explosionDamage);
            }
        }
    }
}
