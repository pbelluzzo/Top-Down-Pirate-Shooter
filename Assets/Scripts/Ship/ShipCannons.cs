using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannons : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private GameObject frontalCannon;
    [SerializeField] private GameObject[] leftCannons;
    [SerializeField] private GameObject[] rightCannons;

    private ShipController shipController;

    private void Awake()
    {
        shipController = GetComponent<ShipController>();

        shipController.FrontCannonShoot += ShipController_FrontCannonShoot;
        shipController.ShootLeftCannons += ShipController_ShootLeftCannons;
        shipController.ShootRightCannons += ShipController_ShootRightCannons;
    }

    private void ShipController_ShootRightCannons()
    {
        ShootRightCannons();
    }

    private void ShipController_ShootLeftCannons()
    {
        ShootLeftCannons();
    }

    private void ShipController_FrontCannonShoot()
    {
        ShootFrontalCannon();
    }

    public void ShootFrontalCannon()
    {
        Instantiate(cannonBall, frontalCannon.transform.position, frontalCannon.transform.rotation);
    }
    public void ShootRightCannons()
    {
        foreach (GameObject cannon in rightCannons)
        {
            Instantiate(cannonBall, cannon.transform.position, cannon.transform.rotation);
        }
    }
    public void ShootLeftCannons()
    {
        foreach(GameObject cannon in leftCannons)
        {
            Instantiate(cannonBall, cannon.transform.position, cannon.transform.rotation);
        }
        
    }
}
