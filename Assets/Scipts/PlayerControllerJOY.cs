using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerJOY : MonoBehaviour
{
    public float speed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Gamepad.current != null)
        {
            moveInput = Gamepad.current.leftStick.ReadValue();

            if (Gamepad.current.buttonSouth.wasPressedThisFrame) // ปุ่ม A บน Xbox หรือ X บน PlayStation
            {
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}