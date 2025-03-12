using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float majorAxis = 5f; // ความยาวครึ่งแกนใหญ่ของวงรี (แกน X)
    public float minorAxis = 2f; // ความยาวครึ่งแกนเล็กของวงรี (แกน Y)
    public float speed = 1f; // ความเร็วในการเคลื่อนที่

    private float angle = 0f; // ตัวแปรที่ใช้ในการคำนวณตำแหน่งของบอส
    private bool movingForward = true; // ทิศทางการเคลื่อนที่

    void Update()
    {
        MoveInEllipticalPath();
    }

    // ฟังก์ชันที่ทำให้บอสเคลื่อนที่ในรูปแบบครึ่งวงรีไปกลับ
    void MoveInEllipticalPath()
    {
        // คำนวณตำแหน่งใหม่บนวงรี โดยใช้ Sin และ Cos
        float x = majorAxis * Mathf.Cos(angle);
        float y = minorAxis * Mathf.Sin(angle);

        // กำหนดตำแหน่งใหม่ของบอส
        transform.position = new Vector3(x, y, transform.position.z);

        // เพิ่มหรือลดค่ามุมขึ้นอยู่กับทิศทาง
        if (movingForward)
        {
            angle += speed * Time.deltaTime;
            if (angle >= Mathf.PI) // ถ้าบอสไปถึงจุดสุดท้ายของครึ่งวงรี
            {
                movingForward = false; // เปลี่ยนทิศทางเป็นกลับ
            }
        }
        else
        {
            angle -= speed * Time.deltaTime;
            if (angle <= 0f) // ถ้าบอสกลับมาถึงจุดเริ่มต้น
            {
                movingForward = true; // เปลี่ยนทิศทางเป็นไปข้างหน้า
            }
        }
    }
}