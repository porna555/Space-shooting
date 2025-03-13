using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Elliptical Movement")]
    public float majorAxis = 5f; // ความยาวครึ่งแกนใหญ่ของวงรี (แกน X)
    public float minorAxis = 2f; // ความยาวครึ่งแกนเล็กของวงรี (แกน Y)
    public float speed = 1f; // ความเร็วในการเคลื่อนที่

    private float angle = 0f; // ตัวแปรที่ใช้ในการคำนวณตำแหน่งของบอส
    private bool movingForward = true; // ทิศทางการเคลื่อนที่

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;  // พรีแฟบของกระสุน
    public Transform firePoint;      // จุดที่ยิงกระสุนออกไป
    public int bulletCount = 10;     // จำนวนกระสุนต่อการยิง
    public float fireRate = 2f;      // ความถี่ในการยิง
    public float bulletSpeed = 3f;   // ความเร็วเริ่มต้นของกระสุน
    public float expandRate = 0.5f;  // อัตราการขยายตัวของกระสุน

    private float nextFireTime;

    void Update()
    {
        MoveInEllipticalPath();

        if (Time.time >= nextFireTime)
        {
            FireBulletWave();
            nextFireTime = Time.time + fireRate;
        }
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

    // ฟังก์ชันยิงกระสุนเป็นครึ่งวงกลม
    void FireBulletWave()
    {
        float angleStep = 260f / (bulletCount - 1); // กระจายเป็นครึ่งวงกลม
        float startAngle = 60f; // เปลี่ยนจาก -90 เป็น 90 องศา (ยิงลงล่าง)

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle - (angleStep * i); // ปรับให้ยิงลงล่าง
            Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet_Boss bulletScript = bullet.GetComponent<Bullet_Boss>();
            bulletScript.SetDirection(direction, bulletSpeed, expandRate);
        }
    }
}