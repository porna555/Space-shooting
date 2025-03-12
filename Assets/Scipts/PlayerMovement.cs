using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float maxForce = 30f;  // แรงบังคับสูงสุด
    public float minForce = 5f;   // แรงบังคับต่ำสุด
    public float maxSpeed = 12f;  // ความเร็วสูงสุด
    public float minSpeed = 2f;   // ความเร็วต่ำสุด
    public float rotationSpeed = 400f;  // ความเร็วในการหมุน
    public float friction = 2f; // ค่าความเสียดทาน

    public Vector3 cameraOffset = new Vector3(0, 0, -10); // ตำแหน่งกล้อง
    private Rigidbody2D rb;
    private Vector2 moveInput;  // อินพุตจาก Left Stick
    private Vector2 lookInput;  // อินพุตจาก Right Stick

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = friction;  // เพิ่มแรงเสียดทานให้ตัวละคร
    }

    void Update()
    {
        // อัปเดตตำแหน่งกล้องให้ตามตัวละคร
        Camera.main.transform.position = transform.position + cameraOffset;
    }

    void FixedUpdate()
    {
        // ตรวจสอบว่ามีอินพุตการเคลื่อนที่หรือไม่
        if (moveInput.magnitude > 0.1f)
        {
            Vector2 direction = moveInput.normalized;
            float currentForce = Mathf.Lerp(minForce, maxForce, moveInput.magnitude);
            float currentMaxSpeed = Mathf.Lerp(minSpeed, maxSpeed, moveInput.magnitude);

            rb.AddForce(direction * currentForce);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, currentMaxSpeed);
        }

        // การหมุนตัวละคร (ใช้ Right Stick)
        if (lookInput.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    // ฟังก์ชันรับค่าการเคลื่อนที่จาก Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // ฟังก์ชันรับค่าการหมุนจาก Input System
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}