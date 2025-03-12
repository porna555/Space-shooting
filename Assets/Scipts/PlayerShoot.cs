using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // ตัวแบบกระสุน
    public Transform shootPoint;     // จุดที่กระสุนจะถูกยิง
    public float bulletSpeed = 10f;  // ความเร็วของกระสุน
    public float fireRate = 0.1f;   // ระยะเวลาในการยิงแต่ละครั้ง (ยิงเร็วแค่ไหน)

    private float nextFireTime = 0f; // เวลาที่จะสามารถยิงกระสุนได้อีก

    void Update()
    {
        // ตรวจสอบการกดปุ่ม A ค้าง
        if (Input.GetButton("Fire1"))  // ปุ่ม A บนจอยคอนมักจะเป็น "Fire1"
        {
            // ตรวจสอบเวลาเพื่อให้การยิงไม่ถี่เกินไป (ตาม fireRate)
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // ตั้งเวลาใหม่เพื่อยิงได้อีกครั้ง
            }
        }
    }

    void Shoot()
    {
        // สร้างกระสุนใหม่จาก prefab
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // ทำให้กระสุนเคลื่อนที่ไปข้างหน้า
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;  // เคลื่อนที่ตามทิศทางของผู้เล่น
    }
}
