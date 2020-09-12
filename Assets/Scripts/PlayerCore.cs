using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Core/Player Core")]
public class PlayerCore : ShipCore
{
    public override void Act(ShipController controller)
    {
        CheckMoveInput(controller); 
    }

    private void CheckMoveInput(ShipController controller)
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
}
