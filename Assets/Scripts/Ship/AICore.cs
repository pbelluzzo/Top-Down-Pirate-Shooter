using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
//using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Core/AI Core")]
public class AICore : ShipCore
{
    private enum AttackMode
    {
        Cannon,
        Chase
    }
    [SerializeField] private AttackMode attackMode;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    private Transform playerTransform;
    private bool inCooldown;

    public override void Initialize()
    {
        inCooldown = false;
    }

    public override void Act(ShipController controller)
    {
        HandleMovement(controller);
        HandleAttack(controller);
    }

    private void HandleMovement(ShipController controller)
    {
        Debug.DrawRay(controller.transform.position, controller.transform.up * -15f, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, (-1 * controller.transform.up));

        if(hit.transform.gameObject.CompareTag("Player")) playerTransform = hit.transform;

        Move(controller, hit);

    }

    private void Move(ShipController controller, RaycastHit2D hit)
    {
        if (hit.distance > attackRange && hit.transform.gameObject.CompareTag("Player"))
        {
            controller.OnMoveEmitted();
            return;
        }
        if (hit.distance > attackRange && !hit.transform.gameObject.CompareTag("Player"))
        {
            controller.OnMoveEmitted();
            controller.OnRotateEmitted(GetRotationDirection(controller));
            return;
        }
        if (hit.distance <= attackRange && hit.transform.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (hit.distance <= 1f && !hit.transform.gameObject.CompareTag("Player"))
        {
            controller.OnRotateEmitted(GetRotationDirection(controller));
            return;
        }
        if (hit.distance <= attackRange && !hit.transform.gameObject.CompareTag("Player"))
        {
            controller.OnMoveEmitted();
            controller.OnRotateEmitted(GetRotationDirection(controller));
            return;
        }
    }

    private int GetRotationDirection(ShipController controller)
    {
        if(playerTransform != null)
        {
            float rotationFactor = Vector3.SignedAngle(controller.transform.position, playerTransform.position, controller.transform.forward);
            if (rotationFactor < 0) return -1;
            return 1;
        }
        return -1;
    }
    private void HandleAttack(ShipController controller)
    {
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, (-1 * controller.transform.up), attackRange);
        Debug.DrawRay(controller.transform.position, controller.transform.up * ( -1 * attackRange), Color.yellow);
        if (!hit || !CheckPlayerInAttackRange(hit)) return;
        if (attackMode == AttackMode.Cannon) CannonAttack(controller);
        if (attackMode == AttackMode.Chase) ChaseAttack(controller);
    }
    private void CannonAttack(ShipController controller)
    {
        if (!inCooldown)
        {
            controller.OnFrontCannonShoot();
            inCooldown = true;
            controller.StartCoroutine(WaitForCooldown());
            return;
        }
    }
    private void ChaseAttack(ShipController controller)
    {
        controller.OnDamageTaken();
    }
    private bool CheckPlayerInAttackRange(RaycastHit2D hit)
    {
        if (hit.distance <= attackRange && hit.transform.gameObject.CompareTag("Player")) return true;
        return false;
    }

    public override IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        inCooldown = false;
    }

}
