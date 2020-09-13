using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannons : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private GameObject frontalCannon;
    [SerializeField] private GameObject[] leftCannons;
    [SerializeField] private GameObject[] rightCannons;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootFrontalCannon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootRightCannons();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootLeftCannons();
        }
    }
}
