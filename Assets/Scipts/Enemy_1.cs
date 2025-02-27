using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_1 : MonoBehaviour
{
    public string playerTag = "Player"; // Tag ของผู้เล่นที่ศัตรูจะไล่ล่า
    public float moveSpeed = 3f; // ความเร็วในการเคลื่อนที่
    public float stoppingDistance = 1f; // ระยะห่างที่ศัตรูจะหยุดเมื่อใกล้ผู้เล่น
    public float turnSpeed = 5f; // ความเร็วในการหันไปหาผู้เล่น
    public float predictionTime = 0.3f; // เวลาคาดการณ์ในอนาคต
    public float randomizeAngle = 15f; // ค่าเบี่ยงเบนมุมในการหมุน (จะทำให้ทิศทางการเคลื่อนที่ของแต่ละตัวแตกต่างกัน)
    public GameObject bulletPrefab; // ตัว Prefab ของกระสุน
    public Transform firePoint; // จุดที่กระสุนจะถูกยิงออกมา
    public float fireRate = 1f; // อัตราการยิงกระสุน (ยิงต่อวินาที)
    public int maxBullets = 1000; // จำนวนกระสุนสูงสุดที่ยิงได้

    private Transform target; // เป้าหมายที่ศัตรูจะไล่ล่า (ผู้เล่น)
    private Vector2 targetVelocity; // ความเร็วของผู้เล่น
    private float randomAngleOffset; // ค่าเบี่ยงเบนมุมสำหรับศัตรูแต่ละตัว
    private float nextFireTime = 0f; // เวลาถัดไปที่จะยิง
    private int bulletCount = 0; // จำนวนกระสุนที่ยิงแล้ว

    private void Start()
    {
        // ค้นหาผู้เล่นด้วย Tag "Player"
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
        {
            target = player.transform; // กำหนดให้ target เป็น Transform ของผู้เล่น
        }

        // สุ่มค่าเบี่ยงเบนมุม
        randomAngleOffset = Random.Range(-randomizeAngle, randomizeAngle);
    }

    private void Update()
    {
        // ตรวจสอบว่ามี target (ผู้เล่น) หรือไม่
        if (target != null && bulletCount < maxBullets)
        {
            // คำนวณความเร็วของผู้เล่น
            targetVelocity = target.GetComponent<Rigidbody2D>().velocity;

            MoveTowardsPlayer(); // ให้ศัตรูเคลื่อนที่ไปหาผู้เล่น
            ShootAtPlayer(); // ให้ศัตรูยิงกระสุน
        }
    }

    // ฟังก์ชันเคลื่อนที่ไปหาผู้เล่น
    void MoveTowardsPlayer()
    {
        // คำนวณทิศทางจากศัตรูไปหาผู้เล่น
        Vector2 direction = (target.position - transform.position).normalized;

        // คำนวณระยะทางจากศัตรูไปยังผู้เล่น
        float distance = Vector2.Distance(transform.position, target.position);

        // คำนวณตำแหน่งที่คาดการณ์ในอนาคตของผู้เล่น
        Vector2 predictedPosition = (Vector2)target.position + targetVelocity * predictionTime;

        // คำนวณทิศทางจากศัตรูไปยังตำแหน่งที่คาดการณ์
        Vector2 predictionDirection = (predictedPosition - (Vector2)transform.position).normalized;

        // แก้ไขการหมุนเพื่อให้ศัตรูหันหน้าไปทางผู้เล่น
        float angle = Mathf.Atan2(predictionDirection.y, predictionDirection.x) * Mathf.Rad2Deg;

        // เพิ่มการเบี่ยงเบนมุมเพื่อให้การหมุนของศัตรูแต่ละตัวแตกต่างกัน
        angle += randomAngleOffset;

        // หมุนศัตรูไปหาผู้เล่นอย่างค่อยเป็นค่อยไป
        float step = turnSpeed * Time.deltaTime;
        float newAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, step);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // เคลื่อนที่ไปยังทิศทางที่คำนวณ
        transform.Translate(predictionDirection * moveSpeed * Time.deltaTime, Space.World);

        // วาดเส้น ray ไปหาผู้เล่น (แสดงใน Scene View)
        Debug.DrawLine(transform.position, predictedPosition, Color.green);
    }

    // ฟังก์ชันยิงกระสุน
    void ShootAtPlayer()
    {
        // ตรวจสอบว่าเวลาในการยิงกระสุนถึงแล้ว
        if (Time.time >= nextFireTime)
        {
            // คำนวณทิศทางที่ศัตรูจะยิงกระสุน
            Vector2 shootDirection = (target.position - transform.position).normalized;

            // หมุนกระสุนไปในทิศทางที่คำนวณ
            Quaternion bulletRotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);

            // สร้างกระสุนใหม่และยิงไปตามทิศทาง
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            // เพิ่มจำนวนกระสุนที่ยิงไปแล้ว
            bulletCount++;

            // รีเซ็ตเวลาถัดไปในการยิง
            nextFireTime = Time.time + 1f / fireRate;

            // ถ้าจำนวนกระสุนถึง 1000 นัด จะหยุดยิง
            if (bulletCount >= maxBullets)
            {
                enabled = false; // หยุดการทำงานของสคริปต์นี้
            }
        }
    }
}
