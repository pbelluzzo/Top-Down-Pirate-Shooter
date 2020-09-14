using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Ship Core/AI Core")]
public class AICore : ShipCore
{
    private enum AttackMode
    {
        Cannon,
        Chase
    }
    private enum MoveStates
    {
        Roaming,
        PlayerEngaged,
        TryingToReengage,
        AttackingPlayer
    }
    [SerializeField] private AttackMode attackMode;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    private MoveStates moveState;
    private Transform playerTransform;
    private bool inCooldown;

    public override void Initialize()
    {
        inCooldown = false;
        moveState = MoveStates.Roaming;
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

        if (hit.transform.gameObject.CompareTag("Player")) playerTransform = hit.transform;

        MoveStateHandler(controller, hit);
    }

    private void MoveStateHandler(ShipController controller, RaycastHit2D hit)
    {
        Debug.Log(moveState);
        switch (moveState)
        {
            case MoveStates.Roaming:
                if (hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.PlayerEngaged;
                if (hit.distance > 1f && !hit.transform.gameObject.CompareTag("Player")) MoveAndRotate(controller);
                controller.OnRotateEmitted(GetRotationDirection(controller));
                break;
            case MoveStates.PlayerEngaged:
                if (hit.distance <= 1f && !hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.Roaming;
                if (hit.distance <= attackRange) moveState = MoveStates.AttackingPlayer;
                if (!hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.TryingToReengage;
                if (hit.distance > attackRange && hit.transform.gameObject.CompareTag("Player")) controller.OnMoveEmitted();
                break;
            case MoveStates.TryingToReengage:
                if (hit.distance <= 1f && !hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.Roaming;
                if (hit.distance <= attackRange && hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.AttackingPlayer;
                if (hit.distance > attackRange && hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.AttackingPlayer;
                if (GetDistanceFromPlayer(controller) <= attackRange ) controller.OnRotateEmitted(GetRotationDirection(controller));
                if (GetDistanceFromPlayer(controller) > attackRange) MoveAndRotate(controller);
                break;
            case MoveStates.AttackingPlayer:
                if (hit.distance <= 1f && !hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.Roaming;
                if (hit.distance > attackRange && !hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.TryingToReengage;
                if (hit.distance > attackRange && hit.transform.gameObject.CompareTag("Player")) moveState = MoveStates.PlayerEngaged;
                break;
        }
    }

    private void MoveAndRotate(ShipController controller)
    {
        controller.OnMoveEmitted();
        controller.OnRotateEmitted(GetRotationDirection(controller));
        return;
    }

    private float GetDistanceFromPlayer(ShipController controller)
    {
        float distance = Vector3.Distance(controller.transform.position, playerTransform.position);
        return distance;
    }
    private int GetRotationDirection(ShipController controller)
    {
        if(playerTransform != null)
        {           
            float rotationFactor = Vector3.SignedAngle(controller.transform.position, playerTransform.position, controller.transform.forward.normalized);
            if (rotationFactor < 0) return -1;
            return 1;
        }
        return -1;
    }
    private void HandleAttack(ShipController controller)
    {
        if (attackMode == AttackMode.Cannon) HandleCannonAttack(controller);
        if (attackMode == AttackMode.Chase) HandleChaseAttack(controller);
    }

    private void HandleCannonAttack(ShipController controller)
    {
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, (-1 * controller.transform.up), attackRange);
        Debug.DrawRay(controller.transform.position, controller.transform.up * (-1 * attackRange), Color.yellow);
        if (!hit || !CheckPlayerInAttackRange(hit)) return;
        CannonAttack(controller);
    }

    private void HandleChaseAttack(ShipController controller)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(controller.transform.position, attackRange);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.GetComponent<ShipController>() != null && enemy.gameObject.CompareTag("Player"))
            {
                ChaseAttack(controller);
            }
        }
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
