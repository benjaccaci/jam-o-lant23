using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private float horizontalInput = 0;
    private float verticalInput = 0;
    public int movementSpeed = 0;
    public int rotationSpeed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        rb.velocity = transform.up * Mathf.Clamp01(verticalInput) * movementSpeed;
    }

    private void RotatePlayer()
    {
        float rotation = horizontalInput * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
    }
}
