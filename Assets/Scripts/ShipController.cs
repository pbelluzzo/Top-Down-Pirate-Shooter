using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public delegate void OnMoveEmittedHandler();
    public delegate void OnRotateEmittedHandler(int directionMultiplier);

    public event OnMoveEmittedHandler MoveEmitted;
    public event OnRotateEmittedHandler RotateEmitted;

    [SerializeField] private ShipCore controllerCore;

    private void Update()
    {
        controllerCore.Act(this);
    }

    public void OnMoveEmitted()
    {
        MoveEmitted?.Invoke();
    }
    public void OnRotateEmitted(int directionMultiplier)
    {
        RotateEmitted?.Invoke(directionMultiplier);
    }


}
