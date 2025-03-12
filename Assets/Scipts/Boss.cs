using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่
    public float curveStrength = 2f; // ความแรงของการโค้ง
    public float moveDistance = 10f; // ระยะการเคลื่อนที่

    private Vector3 startPosition; // ตำแหน่งเริ่มต้น
    private bool movingUp = true; // เพื่อบ่งบอกทิศทางการเคลื่อนที่
    private float timeElapsed = 0f; // ตัวแปรจับเวลาการเคลื่อนที่

    void Start()
    {
        startPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
    }

    void Update()
    {
        MoveInUPath();
    }

    void MoveInUPath()
    {
        timeElapsed += Time.deltaTime * moveSpeed; // เพิ่มเวลาในการคำนวณการเคลื่อนที่

        float horizontalMovement = Mathf.PingPong(timeElapsed, moveDistance); // สร้างการเคลื่อนที่ในแนวนอน
        float verticalMovement = Mathf.Sin(timeElapsed * curveStrength) * moveDistance; // การเคลื่อนที่แนวตั้งในลักษณะ U

        // ถ้าบอสต้องการเคลื่อนที่ไปในแนวตั้งให้แน่ใจว่าอยู่ในทิศทางที่ถูกต้อง
        if (horizontalMovement >= moveDistance / 2)
        {
            movingUp = false; // เมื่อเคลื่อนที่ไปข้างหน้า ให้กลับมาลงด้านล่าง
        }
        if (horizontalMovement <= 0)
        {
            movingUp = true; // เมื่อกลับมาถึงจุดเริ่มต้นให้ขึ้นไปใหม่
        }

        if (!movingUp)
        {
            verticalMovement = -verticalMovement; // ถ้ากลับมาด้านล่าง ให้เปลี่ยนทิศทางของการเคลื่อนที่แนวตั้ง
        }

        // ตั้งค่าตำแหน่งใหม่
        transform.position = new Vector3(startPosition.x + horizontalMovement, startPosition.y + verticalMovement, transform.position.z);
    }
}