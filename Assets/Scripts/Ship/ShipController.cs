using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public delegate void OnMoveEmittedHandler();
    public delegate void OnRotateEmittedHandler(int directionMultiplier);
    public delegate void OnFrontalCannonShoot();
    public delegate void OnLeftCannonsShoot();
    public delegate void OnRightCannonsShoot();
    public delegate void OnTakeDamageHandler(float value);
    public delegate void OnHealthThresholdChangedHandler(int threshold);
    public delegate void OnDestroyHandler(ShipController controller, bool isPlayer);

    public event OnDestroyHandler BeingDestroyed;
    public event OnMoveEmittedHandler MoveEmitted;
    public event OnRotateEmittedHandler RotateEmitted;
    public event OnFrontalCannonShoot FrontCannonShoot;
    public event OnLeftCannonsShoot ShootLeftCannons;
    public event OnRightCannonsShoot ShootRightCannons;
    public event OnTakeDamageHandler DamageTaken;
    public event OnHealthThresholdChangedHandler HealthThresholdChanged;


    [SerializeField] private ShipCore controllerCore;

    private void Awake()
    {
        controllerCore.Initialize();   
    }
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
    public void OnDamageTaken(float value = 0)
    {
        DamageTaken?.Invoke(value);
    }
    public void OnHealthThresholdChanged(int threshold)
    {
        HealthThresholdChanged?.Invoke(threshold);
    }
    public void OnBeingDestroyed(bool isPlayer)
    {
        BeingDestroyed?.Invoke(this, isPlayer);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Debug.Log("collision cannon ball");
            float damageValue = collision.gameObject.GetComponent<CannonBall>().GetDamage();
            OnDamageTaken(damageValue);
        }
    }
}
