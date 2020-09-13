﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public delegate void OnMoveEmittedHandler();
    public delegate void OnRotateEmittedHandler(int directionMultiplier);
    public delegate void OnFrontalCannonShoot();
    public delegate void OnLeftCannonsShoot();
    public delegate void OnRightCannonsShoot();

    public event OnMoveEmittedHandler MoveEmitted;
    public event OnRotateEmittedHandler RotateEmitted;
    public event OnFrontalCannonShoot FrontCannonShoot;
    public event OnLeftCannonsShoot ShootLeftCannons;
    public event OnRightCannonsShoot ShootRightCannons;


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
    public void OnFrontCannonShoot()
    {
        FrontCannonShoot?.Invoke();
    }
    public void OnShootLeftCannons()
    {
        ShootLeftCannons?.Invoke();
    }
    public void OnShootRightCannons()
    {
        ShootRightCannons?.Invoke();
    }

}
