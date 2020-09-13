using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Core/Player Core")]
public class PlayerCore : ShipCore
{
    [SerializeField] private float attackCooldown;
    private bool inCooldown;
    public override void Initialize()
    {
        inCooldown = false;
    }
    public override void Act(ShipController controller)
    {
        CheckMoveInputs(controller);
        CheckShotInputs(controller);
    }

    private void CheckMoveInputs(ShipController controller)
    {
        if (Input.GetKey(KeyCode.W))
        {
            controller.OnMoveEmitted();
        }
        if (Input.GetKey(KeyCode.A))
        {
            controller.OnRotateEmitted(1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controller.OnRotateEmitted(-1);
        }
        return;
    }

    private void CheckShotInputs(ShipController controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleFrontAttack(controller);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HandleLeftAttack(controller);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleRightAttack(controller);
        }
    }

    private void HandleFrontAttack(ShipController controller)
    {
        if (!inCooldown)
        {
            controller.OnFrontCannonShoot();
            inCooldown = true;
            controller.StartCoroutine(WaitForCooldown());
        }
    }

    private void HandleLeftAttack(ShipController controller)
    {
        if (!inCooldown)
        {
            controller.OnShootLeftCannons();
            inCooldown = true;
            controller.StartCoroutine(WaitForCooldown());
        }
    }
    private void HandleRightAttack(ShipController controller)
    {
        if (!inCooldown)
        {
            controller.OnShootRightCannons();
            inCooldown = true;
            controller.StartCoroutine(WaitForCooldown());
        }
    }
    public override IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        inCooldown = false;
    }

}
