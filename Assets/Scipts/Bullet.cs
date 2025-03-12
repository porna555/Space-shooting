using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // ความเร็วของกระสุน
    public Vector2 direction = Vector2.up; // ทิศทางการยิง
    public float lifetime = 2f; // เวลาที่กระสุนจะอยู่บนหน้าจอก่อนหายไป

    void Start()
    {
        // เรียกฟังก์ชัน DestroyBullet หลังจาก 2 วินาที
        Invoke("DestroyBullet", lifetime);
    }

    void Update()
    {
        // ให้กระสุนเคลื่อนที่ไปตามทิศทาง
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        DestroyBullet(); // ทำลายกระสุนเมื่อออกนอกจอ
    }

    // ฟังก์ชันทำลายกระสุน
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    
}
