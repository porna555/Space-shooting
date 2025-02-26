using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public Vector2 direction = Vector2.down; // ทิศทางเริ่มต้นของศัตรู
    public float speed = 5f; // ความเร็วของศัตรู

    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform firePoint; // จุดที่ยิงกระสุน
    public float fireRate = 1.5f; // ระยะเวลาระหว่างการยิง (วินาที)

    public float moveRange = 5f; // ระยะที่ศัตรูจะเคลื่อนที่ไป-กลับ
    private Vector2 startPosition; // ตำแหน่งเริ่มต้นของศัตรู
    private float fireTimer; // ตัวจับเวลาเพื่อยิงกระสุน

    private LineRenderer lineRenderer; // สำหรับแสดงเส้น Ray

    void Start()
    {
        startPosition = transform.position; // บันทึกตำแหน่งเริ่มต้นของศัตรู

        // สร้าง LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        // เคลื่อนที่ตามทิศทางที่กำหนด
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;

        // ตรวจสอบว่าถึงขอบเขตที่กำหนดหรือยัง
        if (Vector2.Distance(startPosition, transform.position) >= moveRange)
        {
            direction *= -1; // กลับทิศทาง
        }

        // ยิงกระสุนตามเวลา
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate; // รีเซ็ตเวลา
        }

        // อัปเดตเส้น Ray แสดงการเคลื่อนที่
        UpdateRay();
    }

    void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // กำหนดทิศทางกระสุนให้พุ่งออกจาก FirePoint
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.direction = firePoint.up;
            }

            // แสดงเส้น Ray การยิงกระสุน
            StartCoroutine(ShowFireRay());
        }
    }

    void UpdateRay()
    {
        // แสดงเส้นทางที่ศัตรูจะเคลื่อนที่ไป
        Vector3 endPosition = startPosition + (Vector2)(direction.normalized * moveRange);
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    IEnumerator ShowFireRay()
    {
        LineRenderer fireRay = new GameObject("FireRay").AddComponent<LineRenderer>();
        fireRay.startWidth = 0.05f;
        fireRay.endWidth = 0.05f;
        fireRay.material = new Material(Shader.Find("Sprites/Default"));
        fireRay.startColor = Color.yellow;
        fireRay.endColor = Color.yellow;

        fireRay.SetPosition(0, firePoint.position);
        fireRay.SetPosition(1, firePoint.position + firePoint.up * 2f);

        yield return new WaitForSeconds(0.2f);
        Destroy(fireRay.gameObject);
    }
}