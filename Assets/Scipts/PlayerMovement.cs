using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // ความเร็วในการเคลื่อนที่
    public float rotationSpeed = 700f;  // ความเร็วในการหมุน (degrees per second)
    public Transform player;  // ตัวแปรที่เก็บตำแหน่งผู้เล่น
    public Vector3 offset;    // ค่าตำแหน่งที่ห่างจากผู้เล่น (ความสูง/ระยะห่าง)

    private Vector2 targetPosition;  // ตำแหน่งที่ตัวละครจะไป

    void Update()
    {
        // เช็คว่ามีการคลิกเมาส์หรือไม่
        if (Input.GetMouseButtonDown(0))
        {
            // แปลงตำแหน่งที่คลิกในโลก 2D ให้เป็นตำแหน่งในเกม
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector2(targetPosition.x, targetPosition.y); // เอาแค่ X และ Y
        }

        // เคลื่อนที่ตัวละครไปยังตำแหน่งที่คลิก
        if ((Vector2)transform.position != targetPosition)
        {
            // เคลื่อนที่ไปยังตำแหน่งที่คลิก
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // คำนวณทิศทางการหมุนไปยังตำแหน่งที่คลิก
            Vector2 direction = targetPosition - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // คำนวณมุมที่หมุน
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));  // สร้างการหมุน
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // หมุนตัวละครไปยังทิศทางที่กำหนด
        }

        // อัปเดตตำแหน่งของกล้องให้ตามผู้เล่น
        Camera.main.transform.position = transform.position + offset;
    }
}
