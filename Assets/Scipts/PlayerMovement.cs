using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วตัวละคร
    public Transform player;  // อ้างอิงตำแหน่งของผู้เล่น (ใช้สำหรับการติดตามกล้อง)
    public Vector3 offset; // ระยะห่างระหว่างกล้องและผู้เล่น
    public float smoothSpeed = 0.125f; // ความเร็วในการติดตาม

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // รับค่าการกดปุ่มจากแกน X และ Y
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ป้องกันการเคลื่อนที่เร็วขึ้นเมื่อเดินแนวทแยง
        movement = movement.normalized;

        // เช็คว่ากำลังเคลื่อนที่อยู่หรือไม่
        if (movement != Vector2.zero)
        {
            // คำนวณมุมหมุนโดยใช้ Atan2 และแปลงค่ามุมจากเรเดียนเป็นองศา
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // หมุนตัวละครให้หันไปในทิศทางที่เคลื่อนที่ (แกน Z ใช้สำหรับ 2D)
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        // เรียกใช้การติดตามกล้อง (ในส่วนของ Update)
        FollowCamera();
    }

    void FixedUpdate()
    {
        // เคลื่อนที่โดยใช้ Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }

    void FollowCamera()
    {
        // คำนวณตำแหน่งที่กล้องควรจะไป
        Vector3 desiredPosition = transform.position + offset;

        // ทำให้การเคลื่อนที่ของกล้องเรียบขึ้น
        Vector3 smoothedPosition = Vector3.Lerp(Camera.main.transform.position, desiredPosition, smoothSpeed);

        // อัพเดตตำแหน่งกล้อง
        Camera.main.transform.position = smoothedPosition;
    }
}
