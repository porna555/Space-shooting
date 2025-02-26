using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxForce = 20f;  // แรงบังคับสูงสุด
    public float minForce = 5f;   // แรงบังคับต่ำสุด
    public float maxSpeed = 8f;   // ความเร็วสูงสุด
    public float minSpeed = 2f;   // ความเร็วต่ำสุดเมื่อเข้าใกล้
    public float rotationSpeed = 400f;  // ความเร็วในการหมุน
    public float friction = 2f; // ค่าความเสียดทาน

    public Vector3 cameraOffset = new Vector3(0, 0, -10);  // ตำแหน่งกล้อง
    private Rigidbody2D rb;
    private bool isMoving = false;

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
        // คำนวณตำแหน่งของเมาส์บนโลก
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - rb.position).normalized; // คำนวณทิศทางจากตำแหน่งตัวละครไปยังเมาส์

        // คำนวณระยะทางจากตัวละครไปยังตำแหน่งเมาส์
        float distance = Vector2.Distance(rb.position, mousePosition);

        // คำนวณอัตราเร่งตามระยะทาง (เมื่อไกลให้เร่ง, เมื่อใกล้ให้ลด)
        float speedFactor = Mathf.Clamp(distance / 5f, 0.2f, 1f);
        float currentForce = Mathf.Lerp(minForce, maxForce, speedFactor);
        float currentMaxSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);

        // เพิ่มแรงไปยัง Rigidbody2D
        rb.AddForce(direction * currentForce);

        // จำกัดความเร็วสูงสุด (ให้สัมพันธ์กับระยะทาง)
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, currentMaxSpeed);

        // ปรับการหมุนของตัวละครให้ไปในทิศทางของเมาส์
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle -90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}