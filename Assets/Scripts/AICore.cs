using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
//using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Core/AI Core")]
public class AICore : ShipCore
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private bool hasCannon;
    [SerializeField] private bool inCooldown = false;
    public override void Act(ShipController controller)
    {
        HandleMovement(controller);
    }

    private void HandleMovement(ShipController controller)
    {
         Debug.DrawRay(controller.transform.position, controller.transform.up * -15f, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, (-1 * controller.transform.up));

        if (hit.distance > 4f && hit.transform.gameObject.CompareTag("Player"))
        {
            //Debug.Log("PLAYER FAR AHEAD, MOVE FORWARD");
            controller.OnMoveEmitted();
            return;
        }
        if (hit.distance > 4f && !hit.transform.gameObject.CompareTag("Player"))
        {
            //Debug.Log("SOMETHING FAR AHEAD, MOVE FORWARD AND ROTATE");
            controller.OnMoveEmitted();
            controller.OnRotateEmitted(GetRotationDirection());
            return;
        }
        if (hit.distance <= 4f && hit.transform.gameObject.CompareTag("Player"))
        {
            // Debug.Log("PLAYER ON SHOOT RANGE, ATTACK!");
            HandleAttack(controller);
            return;
        }
        if (hit.distance <= 1f && !hit.transform.gameObject.CompareTag("Player"))
        {
            //Debug.Log("SOMETHING DANGER CLOSE, ROTATE");
            controller.OnRotateEmitted(GetRotationDirection());
            return;
        }
        if (hit.distance <= 4f && !hit.transform.gameObject.CompareTag("Player"))
        {
            //Debug.Log("SOMETHING NEAR, MOVE FORWARD AND ROTATE");
            controller.OnMoveEmitted();
            controller.OnRotateEmitted(GetRotationDirection());
            return;
        }

    }

    private int GetRotationDirection()
    {
        if (UnityEngine.Random.Range(0, 1) <= 0.5) return 1;
        return -1;
    }

    private void HandleAttack(ShipController controller)
    {
        Debug.Log(inCooldown);
        Debug.Log(hasCannon);
        if (hasCannon && !inCooldown)
        {
            controller.OnFrontCannonShoot();
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
