using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D shipRigidbody;

    private void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rotate();
        }
    }
    public void MoveForward()
    {
        Vector3 pretendedPosition = transform.position - transform.up * movementSpeed * Time.deltaTime;
        shipRigidbody.MovePosition(pretendedPosition);
    }

    public void Rotate()
    {
        Quaternion pretendedRotation = GetPretendedRotation();

        shipRigidbody.SetRotation(pretendedRotation);
    }

    private Quaternion GetPretendedRotation()
    {
        Quaternion currentRotation = transform.rotation;
        float rotateAmount = (rotationSpeed * Time.deltaTime);
        Quaternion pretendedRotation = currentRotation;
        pretendedRotation.eulerAngles = currentRotation.eulerAngles += new Vector3(0, 0, rotateAmount);
        return pretendedRotation;
    }
}
