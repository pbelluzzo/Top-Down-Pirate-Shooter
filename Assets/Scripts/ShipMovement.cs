using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D shipRigidbody;
    private ShipController shipController;

    private void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody2D>();
        shipController = GetComponent<ShipController>();

        shipController.MoveEmitted += ShipController_MoveEmitted;
        shipController.RotateEmitted += ShipController_RotateEmitted;
    }

    private void ShipController_RotateEmitted(int directionMultiplier)
    {
        Rotate(directionMultiplier);
    }

    private void ShipController_MoveEmitted()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        Vector3 pretendedPosition = transform.position - transform.up * movementSpeed * Time.deltaTime;
        shipRigidbody.MovePosition(pretendedPosition);
    }

    public void Rotate(int directionMultiplier)
    {
        Quaternion pretendedRotation = GetPretendedRotation(directionMultiplier);

        shipRigidbody.SetRotation(pretendedRotation);
    }

    private Quaternion GetPretendedRotation(int directionMultiplier)
    {
        Quaternion currentRotation = transform.rotation;
        float rotateAmount = directionMultiplier * (rotationSpeed * Time.deltaTime);
        Quaternion pretendedRotation = currentRotation;
        pretendedRotation.eulerAngles = currentRotation.eulerAngles += new Vector3(0, 0, rotateAmount);
        return pretendedRotation;
    }
}
